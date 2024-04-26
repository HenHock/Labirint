using Runtime.Infrastructure.Bootstrap.GameStateMachine.States.Interfaces;
using Runtime.Infrastructure.Factories;
using Runtime.Services.Save;
using Runtime.Services.SceneLoader;

namespace Runtime.Infrastructure.Bootstrap.GameStateMachine.States
{
    public sealed class LoadLevelSceneState : IPayloadState<int>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly GameFactory _gameFactory;
        private readonly UIFactory _uiFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelSceneState(GameStateMachine gameStateMachine, 
            ISceneLoader sceneLoader, 
            GameFactory gameFactory,
            UIFactory uiFactory,
            IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _progressService = progressService;
        }

        public void Enter(int sceneID)
        {
            _sceneLoader.Load(sceneID, OnLoadedAction);
        }

        private void OnLoadedAction()
        {
            InitLevelWorld();

            _gameStateMachine.Enter<GameplayLoopState>();
        }

        private void InitLevelWorld()
        {
            _gameFactory.CreateHero();
        }
    }
}