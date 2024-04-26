namespace Runtime.Infrastructure.Bootstrap.GameStateMachine.States.Interfaces
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}