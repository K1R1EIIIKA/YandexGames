using System;
using _Scripts.Infrastructure.Core.States;
using UnityEngine;
using UnityEngine.Scripting;
using Zenject;

namespace _Scripts.Infrastructure.Core
{
    [Preserve]
    public class PayloadedStateFactory<TPayload> 
    {
        protected readonly DiContainer _container;

        [Inject]
        public PayloadedStateFactory(DiContainer container)
        {
            _container = container;
            Debug.Log("PayloadedStateFactory get container");
        }
        
        public IPayloadedState<TPayload> Create(Type stateType, GameStateMachine targetStateMachine)
        {
            if (!typeof(IPayloadedState<TPayload>).IsAssignableFrom(stateType))
            {
                throw new ArgumentException($"The type '{stateType}' is not a subclass of State.");
            }
            var state = (IPayloadedState<TPayload>)_container.Instantiate(stateType, new []{targetStateMachine});
            return state;
        }
    }
}