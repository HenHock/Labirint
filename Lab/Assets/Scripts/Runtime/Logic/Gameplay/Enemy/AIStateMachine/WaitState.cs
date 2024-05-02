using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;

namespace Runtime.Logic.Gameplay.Enemy.AIStateMachine
{
    public class WaitState : IState
    {
        private readonly IStateMachine _enemyStateMachine;

        public WaitState(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
        }

        public void Enter() {}
    }
}