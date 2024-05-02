using System.Collections.Generic;
using Runtime.Services.SaveSystem.ProgressService;

namespace Runtime.Services.SaveSystem.SaveLoadService
{
    public interface ISaveLoadService
    {
        List<IProgressWriter> ProgressWriters { get; }
        List<IProgressReader> ProgressReaders { get; }
        
        public void InformProgressReaders();
        public void ProgressWriterUpdates();

        void Save();
        void Cleanup();
        GameProgress Load();
    }
}