using System;
using ModestTree.Util;

namespace _Scripts.Infrastructure.Core.States
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload, Action onLoad = null);
    }
}