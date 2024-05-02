using Runtime.Configs;
using Runtime.Services.Input;
using Runtime.Services.Providers.ConfigsProvider;
using UnityEngine;
using Zenject;

namespace Runtime.Logic.Gameplay.Hero
{
    public class HeroMovement : MonoBehaviour
    {
        private const int SpeedModifier = 20;

        [Header("References")] 
        [SerializeField] private Rigidbody m_Rigidbody;
        
        [Header("View in Inspector")] 
        [SerializeField] private float m_Speed = 10;

        private IInputService _inputService;

        [Inject]
        private void Construct(IConfigProvider configProvider, IInputService inputService)
        {
            _inputService = inputService;
            
            var heroConfig = configProvider.Get<HeroConfig>();
            m_Speed = heroConfig.m_Speed;
        }

        private void FixedUpdate()
        {
            if (_inputService.Move.sqrMagnitude > 0)
                Move();
            else Stop();
        }

        private void Move()
        {
            var direction = new Vector3(_inputService.Move.x, 0, _inputService.Move.y);
            m_Rigidbody.velocity = transform.forward * direction.z * m_Speed * SpeedModifier * Time.deltaTime;
        }

        private void Stop()
        {
            m_Rigidbody.velocity = Vector3.zero;
        }
    }
}