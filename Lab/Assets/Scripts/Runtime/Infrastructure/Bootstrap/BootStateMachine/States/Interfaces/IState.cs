namespace Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}