using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Logic.Gameplay.Enemy.AIStateMachine
{
    public class PatrollingState : IPayloadState<int>
    {
        private readonly IStateMachine _stateMachine;
        private readonly NavMeshAgent _agent;
        private readonly Vector3[] _waypoints;

        private Vector3 Target => _waypoints[WaypointIndex];
        public int WaypointIndex { get; private set; }

        public PatrollingState(EnemyStateMachine stateMachine, NavMeshAgent agent, Vector3[] waypoints)
        {
            _stateMachine = stateMachine;
            _agent = agent;
            _waypoints = waypoints;
        }

        public void Enter(int startWaypoint = 0)
        {
            WaypointIndex = startWaypoint;
            
            if (_waypoints.Length <= 1) 
                _stateMachine.Enter<WaitState>();
            
            _agent.SetDestination(Target);
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
            WaypointIndex++;
            if (WaypointIndex == _waypoints.Length)
            {
                WaypointIndex = 0;
            }
        }
    }
}