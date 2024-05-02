using Runtime.Services.SaveSystem.ProgressService;

namespace Runtime.Services.SaveSystem
{
    public interface IProgressReader
    {
        public void LoadProgress(GameProgress progress);
    }
}