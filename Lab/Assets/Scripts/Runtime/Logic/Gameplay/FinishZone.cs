using System;
using Runtime.Extensions;
using Runtime.Services.WindowService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Runtime.Logic.Gameplay
{
    public class FinishZone : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private TriggerObserver m_TriggerObserver;

        private IWindowService _windowService;

        [Inject]
        private void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Start()
        {
            m_TriggerObserver.OnEnter
                .Subscribe(DetectHero)
                .AddTo(this);
        }

        private void DetectHero(Collider other)
        {
            if (other.IsPlayer()) 
                _windowService.OpenGameOver(true);
        }
    }
}
