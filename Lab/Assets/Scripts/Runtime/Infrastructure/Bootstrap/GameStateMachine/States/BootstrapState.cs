using Runtime.Infrastructure.Bootstrap.GameStateMachine.States.Interfaces;
using Runtime.Services.Providers;
using Runtime.Services.Providers.ConfigsProvider;

namespace Runtime.Infrastructure.Bootstrap.GameStateMachine.States
{
    public sealed class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigProvider _configsProvider;

        public BootstrapState(GameStateMachine gameStateMachine, IAssetProvider assetProvider, IConfigProvider configsProvider)
        {
            _gameStateMachine = gameStateMachine;
            _assetProvider = assetProvider;
            _configsProvider = configsProvider;
        }

        public void Enter()
        {
            _assetProvider.Initialize();
            _configsProvider.Initialize();
            
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}