using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using Runtime.Infrastructure.Factories;
using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.SaveSystem.SaveLoadService;

namespace Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States
{
    public sealed class GameplayLoopState : IState
    {
        private readonly IStateMachine _sceneStateMachine;
        private readonly IConfigProvider _configProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly GameFactory _gameFactory;
        private readonly UIFactory _uiFactory;
        private readonly ISaveLoadService _saveLoadService;

        public GameplayLoopState(SceneStateMachine sceneStateMachine, IConfigProvider configProvider, IAssetProvider assetProvider, 
            GameFactory gameFactory, UIFactory uiFactory, ISaveLoadService saveLoadService)
        {
            _sceneStateMachine = sceneStateMachine;
            _configProvider = configProvider;
            _assetProvider = assetProvider;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            _configProvider.LoadConfigs(ContextType.Gameplay);
            _assetProvider.LoadAssets(ContextType.Gameplay);
            
            InitLevelWorld();
            InitUI();
        }

        public void Exit()
        {
            _configProvider.UnloadConfigs(ContextType.Gameplay);
            _assetProvider.UnloadAssets(ContextType.Gameplay);
            
            _gameFactory.Cleanup();
            _uiFactory.Cleanup();
            
            _saveLoadService.Save();
            _saveLoadService.Cleanup();
        }

        private void InitLevelWorld()
        {
            _gameFactory.CreateMap();
            _gameFactory.CreateHero();
            _gameFactory.CreateEnemies();
            _gameFactory.CreateFinishZone();
            _gameFactory.CreateFollowCamera();
        }

        private void InitUI()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateGameplayHUD();
        }
    }
}