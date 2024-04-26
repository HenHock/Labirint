using Runtime.Configs;
using Runtime.Infrastructure.Bootstrap.GameStateMachine.States.Interfaces;
using Runtime.Infrastructure.Factories;
using Runtime.Services;
using Runtime.Services.Providers;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.SceneLoader;
using UniRx;

namespace Runtime.Infrastructure.Bootstrap.GameStateMachine.States
{
    public sealed class LoadMenuSceneState : IPayloadState<int>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly UIFactory _uiFactory;
        private readonly IConfigProvider _configsProvider;

        public LoadMenuSceneState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, UIFactory uiFactory, IConfigProvider configsProvider)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _configsProvider = configsProvider;
        }

        public void Enter(int sceneID) => _sceneLoader.Load(sceneID, OnLoadedAction);

        private void OnLoadedAction() => InitUI();

        private void InitUI()
        {
            var uiRoot = _uiFactory.CreateUIRoot();
            var mainMenu = _uiFactory.CreateMainMenu(uiRoot);

            // mainMenu.PlayButton
            //     .OnClickAsObservable()
            //     .Subscribe(_ => LoadGameplayScene())
            //     .AddTo(mainMenu.PlayButton);
        }

        private void LoadGameplayScene() => 
            _gameStateMachine.Enter<LoadLevelSceneState, int>(_configsProvider.Get<GameConfig>().m_GameplayScene);
    }
}