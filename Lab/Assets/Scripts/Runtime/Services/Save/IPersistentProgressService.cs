namespace Runtime.Services.Save
{
    public interface IPersistentProgressService
    {
        public GameProgress Progress { get; set; }
    }
}