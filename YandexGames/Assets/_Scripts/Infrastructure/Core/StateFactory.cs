using System;
using _Scripts.Infrastructure.Core.States;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core
{
    public class StateFactory
    {
        protected readonly DiContainer _container;

        [Inject]
        public StateFactory(DiContainer container)
        {
            _container = container;
            Debug.Log("StateFactory get container");
        }

        public IState Create(Type stateType, GameStateMachine targetStateMachine)
        {
            if (!typeof(IState).IsAssignableFrom(stateType))
            {
                throw new ArgumentException($"The type '{stateType}' is not a subclass of State.");
            }

            var state = (IState) _container.Instantiate(stateType, new[]
            {
                targetStateMachine
            });

            return state;
        }
    }
}