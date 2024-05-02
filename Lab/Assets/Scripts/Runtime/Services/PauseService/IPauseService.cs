namespace Runtime.Services.PauseService
{
    public interface IPauseService
    {
        bool IsPaused { get; }
        void Pause();
        void Play();
    }
}