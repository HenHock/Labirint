using Runtime.Services.SaveSystem;
using Runtime.Services.SaveSystem.ProgressService;
using UnityEngine;

namespace Runtime.Logic.UI.GameTimer
{
    public class TimerProgressHandler : MonoBehaviour, IProgressWriter
    {
        [SerializeField] private TimerDisplay m_TimerDisplay;
        
        public void LoadProgress(GameProgress progress) => 
            m_TimerDisplay.Timer = progress.m_WorldData.m_TimerValue;

        public void UpdateProgress(GameProgress progress) => 
            progress.m_WorldData.m_TimerValue = m_TimerDisplay.Timer;
    }
}
