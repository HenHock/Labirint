using Runtime.Infrastructure.Bootstrap.GameStateMachine.States.Interfaces;
using UniRx;

namespace Runtime.Infrastructure.Bootstrap.GameStateMachine
{
    public interface IStateMachine
    {
        IReadOnlyReactiveProperty<IExitableState> CurrentState { get; }
        void RegisterState<TState>(TState state) where TState : IExitableState;
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
    }
}