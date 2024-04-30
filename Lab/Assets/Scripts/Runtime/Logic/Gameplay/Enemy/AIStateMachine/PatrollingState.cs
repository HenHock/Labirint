using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using IState = Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces.IState;

namespace Runtime.Logic.Gameplay.Enemy.AIStateMachine
{
    public class PatrollingState : IState
    {
        private readonly EnemyStateMachine _stateMachine;
        private readonly NavMeshAgent _agent;
        private readonly Vector3[] _waypoints;

        private Vector3 Target => _waypoints[_waypointIndex];
        private int _waypointIndex;

        public PatrollingState(EnemyStateMachine stateMachine, NavMeshAgent agent, Vector3[] waypoints)
        {
            _stateMachine = stateMachine;
            _agent = agent;
            _waypoints = waypoints;
        }

        public void Enter()
        {
            if (_waypoints.Length <= 1) 
                _stateMachine.Enter<WaitState>();
        }

        public void Update()
        {
            if ((Target - _agent.transform.position).sqrMagnitude < 0.1f)
            {
                IterateWaypointIndex();
                _agent.SetDestination(Target);
            }
        }

        private void IterateWaypointIndex()
        {
            _waypointIndex++;
            if (_waypointIndex == _waypoints.Length)
            {
                _waypointIndex = 0;
            }
        }
    }
}