using System.Collections.Generic;
using Runtime.Extensions;
using Runtime.Services.SaveSystem.ProgressService;
using UnityEngine;

namespace Runtime.Services.SaveSystem.SaveLoadService
{
    public sealed class SaveLoadService : ISaveLoadService
    {
        private const string GameProgressPrefs = "GameProgress";

        public List<IProgressWriter> ProgressWriters { get; } = new();
        public List<IProgressReader> ProgressReaders { get; } = new();

        private readonly IPersistentProgressService _progressService;

        public SaveLoadService(IPersistentProgressService progressService) => 
            _progressService = progressService;

        public void Save()
        {
            // Instead PlayerPrefs, you can write json to a file.
            PlayerPrefs.SetString(GameProgressPrefs, _progressService.Progress.ToJson());
            Debug.Log("Saved data");
        }

        public GameProgress Load() => 
            PlayerPrefs.GetString(GameProgressPrefs).FromJson<GameProgress>();

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public void ProgressWriterUpdates()
        {
            foreach (var progressWriter in ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            Debug.Log("Update save slot");
        }

        public void InformProgressReaders()
        {
            foreach (var progressReader in ProgressReaders) 
                progressReader.LoadProgress(_progressService.Progress);

            Debug.Log("Load data from save slot");
        }
    }
}