namespace Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces
{
    public interface IExitableState
    {
        public void Exit() {}
        public void Update() {}
    }
}