using System;
using System.Linq;
using Runtime.Data;
using Runtime.Infrastructure.Factories;
using Runtime.Logic.Gameplay.Enemy.AIStateMachine;
using Runtime.Services.SaveSystem;
using Runtime.Services.SaveSystem.ProgressService;
using UnityEngine;
using Zenject;

namespace Runtime.Logic.Gameplay.Enemy
{
    /// <summary>
    /// Component for save and load transform data.
    /// </summary>
    public class EnemyProgressHandler : MonoBehaviour, IProgressWriter
    {
        [SerializeField] private UniqueID m_UniqueID;
        [SerializeField] private EnemyAI m_EnemyAI;
        
        private GameFactory _gameFactory;

        [Inject]
        private void Construct(GameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public void LoadProgress(GameProgress progress)
        {
            if (SaveDataExist(progress, out EnemyStateData data)) 
                LoadStateData(data);
        }

        public void UpdateProgress(GameProgress progress)
        {
            if (SaveDataExist(progress, out EnemyStateData data))
            {
                data.m_State = GetCurrentState();
                data.m_StateProgress = GetStateProgress();
            }
            else CreateSaveData(progress);
        }

        private void CreateSaveData(GameProgress progress) =>
            progress.m_WorldData.m_EnemyDataList.Add(new EnemyStateData(m_UniqueID.m_ID, GetCurrentState(), GetStateProgress()));

        private bool SaveDataExist(GameProgress progress, out EnemyStateData data)
        {
            data = progress.m_WorldData.m_EnemyDataList.FirstOrDefault(d => d.m_UniqueID == m_UniqueID.m_ID);
            return data != null;
        }

        private void LoadStateData(EnemyStateData data)
        {
            switch (data.m_State)
            {
                case nameof(WaitState):
                    m_EnemyAI.StateMachine.Enter<WaitState>();
                    break;
                case nameof(PatrollingState):
                    m_EnemyAI.StateMachine.Enter<PatrollingState, int>(Convert.ToInt16(data.m_StateProgress));
                    break;
                case nameof(ChasingState):
                    m_EnemyAI.StateMachine.Enter<ChasingState, Transform>(_gameFactory.Hero);
                    break;
            }
        }

        private string GetCurrentState() => 
            m_EnemyAI.StateMachine.CurrentState.Value.GetType().Name;

        private string GetStateProgress()
        {
            if (m_EnemyAI.StateMachine.CurrentState.Value is PatrollingState patrollingState)
                return patrollingState.WaypointIndex.ToString();
            
            return "";
        }
    }
}