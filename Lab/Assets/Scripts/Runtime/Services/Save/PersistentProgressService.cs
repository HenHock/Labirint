namespace Runtime.Services.Save
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public GameProgress Progress { get; set; }
    }
}