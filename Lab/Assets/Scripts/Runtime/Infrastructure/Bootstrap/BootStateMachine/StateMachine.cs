using System;
using System.Collections.Generic;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using UniRx;

namespace Runtime.Infrastructure.Bootstrap.BootStateMachine
{
    public abstract class StateMachine : IStateMachine
    {
        public IReadOnlyReactiveProperty<IExitableState> CurrentState => _currentState;

        private readonly Dictionary<Type, IExitableState> _registeredState = new();
        private readonly ReactiveProperty<IExitableState> _currentState = new();

        protected StateMachine()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => CurrentState.Value.Update());
        }

        public void RegisterState<TState>(TState state) where TState : IExitableState
        {
            var key = typeof(TState);
            _registeredState.Add(key, state);
        }

        public void Enter<TState>() where TState : class, IState
        {
            if (IsNotCurrentState<TState>())
            {
                TState newState = ChangeState<TState>();
                newState?.Enter();
            }
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            if (IsNotCurrentState<TState>())
            {
                TState state = ChangeState<TState>();
                state.Enter(payload);
            }
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

        private bool IsNotCurrentState<TState>() where TState : class, IExitableState => 
            CurrentState.GetType() != typeof(TState);
    }
}