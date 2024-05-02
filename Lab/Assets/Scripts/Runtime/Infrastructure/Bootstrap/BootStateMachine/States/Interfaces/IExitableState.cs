namespace Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces
{
    public interface IExitableState
    {
        public void Exit() {}
        
        /// <summary>
        /// By default isn't invoke for all states. To activate should be invoke RegisterUpdateMethod from IStateMachine.
        /// </summary>
        public void Update() {}
    }
}