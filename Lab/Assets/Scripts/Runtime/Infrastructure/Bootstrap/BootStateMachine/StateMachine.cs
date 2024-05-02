using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using UniRx;

namespace Runtime.Infrastructure.Bootstrap.BootStateMachine
{
    public abstract class StateMachine : IStateMachine
    {
        public IReadOnlyReactiveProperty<IExitableState> CurrentState => _currentState;

        private readonly Dictionary<Type, IExitableState> _registeredState = new();
        private readonly ReactiveProperty<IExitableState> _currentState = new();

        public void RegisterUpdateMethod(CancellationToken lifeTimeToken) =>
            Observable.EveryUpdate()
                .Subscribe(_ => CurrentState.Value.Update())
                .AddTo(lifeTimeToken);

        public void RegisterState<TState>(TState state) where TState : IExitableState
        {
            var key = typeof(TState);
            _registeredState.Add(key, state);
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState newState = ChangeState<TState>();
            newState?.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload = default) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState.Value?.Exit();

            var state = GetState<TState>();
            _currentState.Value = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _registeredState[typeof(TState)] as TState;
    }
}