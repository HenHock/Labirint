namespace Runtime.Services.SaveSystem.ProgressService
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public GameProgress Progress { get; set; }
    }
}