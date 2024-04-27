using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using Runtime.Infrastructure.Factories;
using Runtime.Services.SceneLoader;

namespace Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States
{
    public sealed class LoadMenuSceneState : IPayloadState<int>
    {
        private readonly StateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly UIFactory _uiFactory;

        public LoadMenuSceneState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, UIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
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
            _gameStateMachine.Enter<LoadGameplayState>();
    }
}