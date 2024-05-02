using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.StateFactory;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States;
using Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States;
using Runtime.Services.SaveSystem.SaveLoadService;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private StateFactory _stateFactory;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, StateFactory stateFactory, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _stateFactory = stateFactory;
            _saveLoadService = saveLoadService;
        }
        
        private void Start()
        {
            DontDestroyOnLoad(this);
            
            _gameStateMachine.RegisterState(_stateFactory.Create<BootstrapState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadGameplayState>());
            
            _gameStateMachine.Enter<BootstrapState>();
        }

        private void OnApplicationQuit() => _saveLoadService.Save();
    }
}