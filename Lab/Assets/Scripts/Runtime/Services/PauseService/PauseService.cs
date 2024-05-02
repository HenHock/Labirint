using UnityEngine;

namespace Runtime.Services.PauseService
{
    public sealed class PauseService : IPauseService
    {
        public bool IsPaused => Time.timeScale == 0;

        public void Pause()
        {
            Time.timeScale = 0;
            Debug.Log("Paused the game");
        }

        public void Play()
        {
            Time.timeScale = 1;
            Debug.Log("Unpaused the game");
        }
    }
}