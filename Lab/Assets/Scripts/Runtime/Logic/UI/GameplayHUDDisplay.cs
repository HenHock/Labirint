using Runtime.Services.SaveSystem.ProgressService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Runtime.Logic.UI
{
    public class GameplayHUDDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_CurrentAttemptTextFiled;
        [SerializeField] private TextMeshProUGUI m_WinsTextFiled;
        [SerializeField] private TextMeshProUGUI m_LosesTextFiled;
        
        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }

        private void Start()
        {
            m_CurrentAttemptTextFiled.text = _progressService.Progress.m_StatsData.m_TotalAttemptCount.ToString();
            m_WinsTextFiled.text = _progressService.Progress.m_StatsData.m_WinCount.ToString();
            m_LosesTextFiled.text = _progressService.Progress.m_StatsData.m_LoseCount.ToString();
        }
    }
}
