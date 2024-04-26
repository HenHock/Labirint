using Runtime.Infrastructure.Bootstrap.GameStateMachine.States.Interfaces;

namespace Runtime.Infrastructure.Bootstrap.GameStateMachine.StateFactory
{
    public interface IStateFactory
    {
        TState Create<TState>() where TState : IExitableState;
    }
}