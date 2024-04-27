using Runtime.Configs;
using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.SceneLoader;

namespace Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States
{
    public sealed class LoadGameplayState : IState
    {
        private readonly StateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IConfigProvider _configProvider;

        public LoadGameplayState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, IConfigProvider configProvider)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _configProvider = configProvider;
        }

        public void Enter()
        {
            _sceneLoader.Load(_configProvider.Get<GameConfig>().m_GameplayScene);
        }
    }
}