using ModestTree;
using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using Runtime.Services.SaveSystem.ProgressService;
using Runtime.Services.SaveSystem.SaveLoadService;
using UnityEngine;

namespace Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States
{
    public sealed class LoadProgressState : IState
    {
        private readonly IStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(SceneStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadGameProgressOrInitNew();
            _gameStateMachine.Enter<GameplayLoopState>();
        }

        public void Exit() => Debug.Log("Finished load progress");

        private void LoadGameProgressOrInitNew() => 
            _progressService.Progress = _saveLoadService.Load() ?? new GameProgress();
    }
}