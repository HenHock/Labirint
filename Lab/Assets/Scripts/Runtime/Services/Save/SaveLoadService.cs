using Runtime.Infrastructure.Factories;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Services.Save
{
    public sealed class SaveLoadService : ISaveLoadService
    {
        private const string GameProgressPrefs = "GameProgress";
        
        private readonly IPersistentProgressService _progressService;
        private readonly GameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, GameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void Save()
        {
            // Instead PlayerPrefs, you can write json to a file.
            PlayerPrefs.SetString(GameProgressPrefs, _progressService.Progress.ToJson());
        }

        public GameProgress Load() => 
            PlayerPrefs.GetString(GameProgressPrefs).FromJson<GameProgress>();
    }
}