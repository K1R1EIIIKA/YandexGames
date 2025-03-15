using JetBrains.Annotations;

namespace _Scripts.EventsLogic
{
    public static class EventBus<T> where T : IEvent
    {
        public delegate void Event(T @event);

        public static event Event OnEvent;

        public static void Raise([CanBeNull] T @event) => OnEvent?.Invoke(@event);
    }
}