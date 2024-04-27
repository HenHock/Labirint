using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States;
using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.Providers.ConfigsProvider;

namespace Runtime.Infrastructure.Bootstrap.BootStateMachine.States
{
    public sealed class BootstrapState : IState
    {
        private readonly StateMachine _gameStateMachine;
        private readonly IConfigProvider _configProvider;

        public BootstrapState(GameStateMachine gameStateMachine, IConfigProvider configProvider)
        {
            _gameStateMachine = gameStateMachine;
            _configProvider = configProvider;
        }

        public void Enter()
        {
            _configProvider.LoadConfigs(ContextType.Boot);
            
            _gameStateMachine.Enter<LoadGameplayState>();
        }
    }
}