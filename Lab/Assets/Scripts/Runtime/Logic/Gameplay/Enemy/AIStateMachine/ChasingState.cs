using Runtime.Configs.Enemy;
using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using UnityEngine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using Runtime.Logic.UI;
using Runtime.Services.WindowService;
using UnityEngine.AI;

namespace Runtime.Logic.Gameplay.Enemy.AIStateMachine
{
    public class ChasingState : IPayloadState<Transform>
    {
        private readonly IStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        private readonly NavMeshAgent _agent;
        
        private Transform _target;

        public ChasingState(EnemyStateMachine stateMachine, NavMeshAgent agent, IWindowService windowService)
        {
            _windowService = windowService;
            _stateMachine = stateMachine;
            _agent = agent;
        }

        public void Enter(Transform target)
        {
            _target = target;
        }

        public void Update()
        {
            if (HasTarget())
                if (ReachedTarget())
                {
                    Exit();
                    _windowService.OpenGameOver(false);
                }
                else MoveTo(_target.transform.position);
        }

        public void Exit()
        {
            _target = null;
        }

        private void MoveTo(Vector3 target)
        {
            _agent.SetDestination(target);
        }

        private bool ReachedTarget() => 
            (_target.transform.position - _agent.transform.position).sqrMagnitude < 1;

        private bool HasTarget() => _target != null;

    }
}