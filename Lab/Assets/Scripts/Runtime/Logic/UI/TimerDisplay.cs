using System;
using Runtime.Infrastructure.Factories;
using TMPro;
using UnityEngine;
using Zenject;

namespace Runtime.Logic.UI
{
    public class Timer : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private TextMeshProUGUI m_TimerValueTextField;
        
        [Header("View in Inspector")]
        [SerializeField] private float m_Timer;

        private bool _timerOn;
        private GameFactory _gameFactory;

        [Inject]
        private void Construct(GameFactory gameFactory) => 
            _gameFactory = gameFactory;

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
                    
                    // Game over.
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
