using Runtime.Services.SaveSystem.ProgressService;

namespace Runtime.Services.SaveSystem
{
    public interface IProgressWriter : IProgressReader
    {
        public void UpdateProgress(GameProgress progress);
    }
}