using System;

namespace _Scripts.Infrastructure.Core.States
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload, Action onLoad) where TState : class, IPayloadedState<TPayload>;
    }
}