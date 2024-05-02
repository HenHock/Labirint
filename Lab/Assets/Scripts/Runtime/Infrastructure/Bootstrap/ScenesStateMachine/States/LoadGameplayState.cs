using Runtime.Configs;
using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.SceneLoader;

namespace Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States
{
    public sealed class LoadGameplayState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IConfigProvider _configProvider;

        public LoadGameplayState(IStateMachine stateMachine, ISceneLoader sceneLoader, IConfigProvider configProvider)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _configProvider = configProvider;
        }

        public void Enter()
        {
            _configProvider.LoadConfigs(ContextType.Boot);
            _sceneLoader.Load(_configProvider.Get<GameConfig>().m_GameplayScene);
        }

        public void Exit() => _configProvider.UnloadConfigs(ContextType.Boot);
    }
}