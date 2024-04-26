using Runtime.Infrastructure.Bootstrap.GameStateMachine.StateFactory;
using Runtime.Infrastructure.Bootstrap.GameStateMachine.States;
using Runtime.Services.Save;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine.GameStateMachine _gameStateMachine;
        private StateFactory _stateFactory;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(GameStateMachine.GameStateMachine gameStateMachine, StateFactory stateFactory, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _stateFactory = stateFactory;
            _saveLoadService = saveLoadService;
        }
        
        private void Start()
        {
            DontDestroyOnLoad(this);
            
            _gameStateMachine.RegisterState(_stateFactory.Create<BootstrapState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadProgressState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadMenuSceneState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadLevelSceneState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameplayLoopState>());
            
            _gameStateMachine.Enter<BootstrapState>();
        }

        private void OnApplicationQuit() => _saveLoadService.Save();
    }

}