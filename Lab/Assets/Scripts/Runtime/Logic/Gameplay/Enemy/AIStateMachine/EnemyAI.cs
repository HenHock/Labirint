using System;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Runtime.Configs.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Logic.Gameplay.Enemy.AIStateMachine
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private NavMeshAgent m_Agent;
        [SerializeField] private VisionCone m_VisionCone;

        [Header("View in Inspector")] 
        [SerializeField, ReadOnly] private string m_CurrentState = nameof(WaitState);
        [SerializeField, ReadOnly] private bool m_HeroIsDetected;

        private Vector3[] _waypoints = Array.Empty<Vector3>();

        public EnemyStateMachine StateMachine { get; private set; }

        public void Construct(EnemyConfig enemyConfig, Vector3[] waypoints)
        {
            _waypoints = waypoints;
            m_Agent.speed = enemyConfig.m_Speed;
            
            CreateStateMachine(waypoints);
            SetupVisionCone(enemyConfig);
            RegisterDebugInfo();
        }

        private void CreateStateMachine(Vector3[] waypoints)
        {
            StateMachine = new EnemyStateMachine();
            
            StateMachine.RegisterState(new WaitState(StateMachine));
            StateMachine.RegisterState(new PatrollingState(StateMachine, m_Agent, waypoints));
            StateMachine.RegisterState(new ChasingState(StateMachine, m_Agent));
            
            StateMachine.Enter<PatrollingState>();
        }

        private void SetupVisionCone(EnemyConfig enemyConfig)
        {
            m_VisionCone.m_VisionAngle = enemyConfig.m_ViewAngle;
            m_VisionCone.m_VisionRange = enemyConfig.m_ViewDistance;

            m_VisionCone.OnDetectedHero
                .Subscribe(OnPlayerDetectedHandler)
                .AddTo(this);
        }

        private void RegisterDebugInfo() =>
            StateMachine.CurrentState
                .Subscribe(currentState => m_CurrentState = currentState.GetType().Name)
                .AddTo(this);

        private void OnPlayerDetectedHandler(Transform heroTransform)
        {
            if (heroTransform != null && !m_HeroIsDetected)
            {
                m_HeroIsDetected = true;
                StateMachine.Enter<ChasingState, Transform>(heroTransform);
            }
            else if (heroTransform == null && m_HeroIsDetected)
            {
                m_HeroIsDetected = false;
                StateMachine.Enter<PatrollingState>();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_waypoints.Length > 1)
            {
                for (var index = 1; index < _waypoints.Length; index++)
                {
                    Gizmos.DrawLine(_waypoints[index - 1], _waypoints[index]);
                    Gizmos.DrawSphere(_waypoints[index], 0.25f);
                }
            }
        }

#endif
    }
}