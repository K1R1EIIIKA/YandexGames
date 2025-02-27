using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.Infrastructure.Core.SceneTransitions;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core.States
{
    public class GameStateMachine : IDisposable, IGameStateMachine
    {
        private readonly StateFactory _stateFactory;
        private IPersistantProgressService _progressService;
        private readonly PayloadedStateFactory<string> _payloadedStateFactory;

        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public GameStateMachine(StateFactory stateFactory, PayloadedStateFactory<string> payloadedStateFactory, ISaveLoadService saveLoadService,
            IPersistantProgressService progressService)
        {
            _stateFactory = stateFactory;
            _payloadedStateFactory = payloadedStateFactory;
            _saveLoadService = saveLoadService;
            _progressService = progressService;

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
            if (_activeState != null && _activeState != _states[typeof(BootstrapState)])
            {
                Debug.Log(_activeState);
                Debug.Log($"Before saving: {_progressService.Progress != null}, data: " +
                          (_progressService.Progress != null ? JsonUtility.ToJson(_progressService.Progress) : "NULL"));

                _saveLoadService.SaveProgress();
            }

            var state = ChangeState<TState>();
            state.Enter();
        }



        public void Dispose()
        {
            _activeState?.Exit();
            _states?.Clear();
        }

        public void Enter<TState, TPayload>(TPayload payload, Action onLoad = null)
            where TState : class, IPayloadedState<TPayload>
        {
            if (_activeState != null && _activeState != _states[typeof(BootstrapState)])
            {
                _saveLoadService.SaveProgress(); // Вызываем в главном потоке
            }

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