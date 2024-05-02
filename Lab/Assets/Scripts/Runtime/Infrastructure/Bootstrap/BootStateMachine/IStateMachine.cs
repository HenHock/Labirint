using System.Threading;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using UniRx;

namespace Runtime.Infrastructure.Bootstrap.BootStateMachine
{
    public interface IStateMachine
    {
        IReadOnlyReactiveProperty<IExitableState> CurrentState { get; }
        
        /// <summary>
        /// Register invoke Update method in registered states as the Unity Update method.
        /// </summary>
        void RegisterUpdateMethod(CancellationToken lifeTimeToken);
        void RegisterState<TState>(TState state) where TState : IExitableState;
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
    }
}