namespace Runtime.Services.SaveSystem.ProgressService
{
    public interface IPersistentProgressService
    {
        public GameProgress Progress { get; set; }
    }
}