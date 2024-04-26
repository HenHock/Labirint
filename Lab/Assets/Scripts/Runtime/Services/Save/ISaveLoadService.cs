namespace Runtime.Services.Save
{
    public interface ISaveLoadService
    {
        void Save();
        GameProgress Load();
    }
}