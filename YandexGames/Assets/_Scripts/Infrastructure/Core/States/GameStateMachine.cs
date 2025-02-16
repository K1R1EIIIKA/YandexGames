using System;
using System.Collections.Generic;
using _Scripts.Infrastructure.Core.SceneTransitions;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core.States
{
    public class GameStateMachine : IDisposable, IGameStateMachine
    {
        private readonly StateFactory _stateFactory;
        private readonly PayloadedStateFactory<string> _payloadedStateFactory;

        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        [Inject]
        public GameStateMachine(StateFactory stateFactory, PayloadedStateFactory<string> payloadedStateFactory)
        {
            _stateFactory = stateFactory;
            _payloadedStateFactory = payloadedStateFactory;

            Debug.Log("GameStateMachine was initialized");

            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = _stateFactory.Create(typeof(BootstrapState), this),
                [typeof(LoadProgressState)] = _stateFactory.Create(typeof(LoadProgressState), this),
                [typeof(LoadLevelState)] = _payloadedStateFactory.Create(typeof(LoadLevelState), this),
                [typeof(GameLoopState)] = _stateFactory.Create(typeof(GameLoopState), this), };

            Debug.Log("States was created");
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Dispose()
        {
            _activeState?.Exit();
            _states?.Clear();
        }

        public void Enter<TState, TPayload>(TPayload payload, Action onLoad = null) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload, onLoad);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}