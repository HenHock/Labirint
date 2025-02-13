﻿using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using Zenject;

namespace Runtime.Infrastructure.Bootstrap.BootStateMachine.StateFactory
{
    public sealed class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container) => _container = container;

        public TState Create<TState>() where TState : IExitableState => 
            _container.Instantiate<TState>();
    }
}