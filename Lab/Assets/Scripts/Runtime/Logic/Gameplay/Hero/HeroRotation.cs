using Runtime.Configs;
using Runtime.Services.Input;
using Runtime.Services.Providers.ConfigsProvider;
using UnityEngine;
using Zenject;

namespace Runtime.Logic.Gameplay.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        private const int RotationModifier = 20;

        [Header("View in Inspector")]
        [SerializeField] private float m_RotationSpeed = 10;

        private IInputService _inputService;

        [Inject]
        private void Construct(IConfigProvider configProvider, IInputService inputService)
        {
            _inputService = inputService;

            var heroConfig = configProvider.Get<HeroConfig>();
            m_RotationSpeed = heroConfig.m_RotationSpeed;
        }

        private void Update()
        {
            if (_inputService.Move.x != 0)
                transform.Rotate(Vector3.up, _inputService.Move.x * m_RotationSpeed * RotationModifier * Time.deltaTime);
        }
    }
}