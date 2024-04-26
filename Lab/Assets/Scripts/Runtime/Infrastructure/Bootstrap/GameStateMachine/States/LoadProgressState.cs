using Runtime.Configs;
using Runtime.Infrastructure.Bootstrap.GameStateMachine.States.Interfaces;
using Runtime.Services.Providers;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.Save;

namespace Runtime.Infrastructure.Bootstrap.GameStateMachine.States
{
    public sealed class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IConfigProvider _configsProvider;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IConfigProvider configsProvider, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _configsProvider = configsProvider;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadGameProgressOrInitNew();
            _gameStateMachine.Enter<LoadMenuSceneState, int>(_configsProvider.Get<GameConfig>().m_MenuScene);
        }

        private void LoadGameProgressOrInitNew() => 
            _progressService.Progress = _saveLoadService.Load() ?? new GameProgress();
    }
}