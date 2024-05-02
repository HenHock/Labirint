using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.StateFactory;
using Runtime.Infrastructure.Bootstrap.ScenesStateMachine;
using Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Bootstrap
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        private IStateMachine _sceneStateMachine;
        private IStateFactory _stateFactory;

        [Inject]
        private void Construct(SceneStateMachine sceneStateMachine, StateFactory stateFactory)
        {
            _sceneStateMachine = sceneStateMachine;
            _stateFactory = stateFactory;
        }

        private void Start()
        {
            _sceneStateMachine.RegisterState(_stateFactory.Create<LoadProgressState>());
            _sceneStateMachine.RegisterState(_stateFactory.Create<GameplayLoopState>());
            _sceneStateMachine.RegisterState(_stateFactory.Create<LoadGameplayState>());
            
            _sceneStateMachine.Enter<LoadProgressState>();
        }
    }
}