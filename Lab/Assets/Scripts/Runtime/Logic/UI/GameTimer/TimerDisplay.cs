using Runtime.Infrastructure.Factories;
using Runtime.Services.WindowService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Runtime.Logic.UI.GameTimer
{
    public class TimerDisplay : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private TextMeshProUGUI m_TimerValueTextField;
        
        [Header("View in Inspector")]
        [SerializeField] private float m_Timer;

        public float Timer
        {
            get => m_Timer;
            set => m_Timer = value;
        }

        private bool _timerOn;
        private GameFactory _gameFactory;
        private IWindowService _windowService;

        [Inject]
        private void Construct(GameFactory gameFactory, IWindowService windowService)
        {
            _windowService = windowService;
            _gameFactory = gameFactory;
        }

        private void Start()
        {
            m_Timer = _gameFactory.CurrentLevelConfig.m_SecondsToCompleteLevel;
            _timerOn = true;
        }

        private void Update()
        {
            if (_timerOn)
            {
                if (m_Timer > 0)
                {
                    m_Timer -= Time.deltaTime;
                }
                else
                {
                    m_Timer = 0;
                    _timerOn = false;
                    
                    _windowService.OpenGameOver(false);
                }

                m_TimerValueTextField.text = FormatTimer(m_Timer);
            }
        }

        private string FormatTimer(float seconds)
        {
            int minutes = (int)(seconds / 60);
            int remainingSeconds = (int)(seconds % 60);
    
            return $"{minutes:D2}:{remainingSeconds:D2}";
        }

    }
}
