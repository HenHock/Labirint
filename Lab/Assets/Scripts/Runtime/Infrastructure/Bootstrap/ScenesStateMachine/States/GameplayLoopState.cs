using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using Runtime.Infrastructure.Factories;
using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.Providers.ConfigsProvider;

namespace Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States
{
    public class GameplayLoopState : IState
    {
        private readonly SceneStateMachine _sceneStateMachine;
        private readonly IConfigProvider _configProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly GameFactory _gameFactory;
        private readonly UIFactory _uiFactory;

        public GameplayLoopState(SceneStateMachine sceneStateMachine, IConfigProvider configProvider, IAssetProvider assetProvider, GameFactory gameFactory, UIFactory uiFactory)
        {
            _sceneStateMachine = sceneStateMachine;
            _configProvider = configProvider;
            _assetProvider = assetProvider;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _configProvider.LoadConfigs(ContextType.Gameplay);
            _assetProvider.LoadAssets(ContextType.Gameplay);
            
            InitLevelWorld();
        }

        public void Exit()
        {
            _configProvider.UnloadConfigs(ContextType.Gameplay);
            _assetProvider.UnloadAssets(ContextType.Gameplay);
            
            _gameFactory.Cleanup();
            _uiFactory.Cleanup();
        }
        
        private void InitLevelWorld()
        {
            _gameFactory.CreateMap();
            _gameFactory.CreateHero();
            _gameFactory.CreateEnemies();
            _gameFactory.CreateFollowCamera();
        }
    }
}