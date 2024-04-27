using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;

namespace Runtime.Infrastructure.Bootstrap.BootStateMachine.StateFactory
{
    public interface IStateFactory
    {
        TState Create<TState>() where TState : IExitableState;
    }
}