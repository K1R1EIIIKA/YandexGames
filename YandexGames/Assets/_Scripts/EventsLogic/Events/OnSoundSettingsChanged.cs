namespace _Scripts.EventsLogic.Events
{
    public class OnSoundSettingsChanged : IEvent
    {
        public bool IsSoundEnabled { get; }

        public OnSoundSettingsChanged(bool isSoundEnabled)
        {
            IsSoundEnabled = isSoundEnabled;
        }
    }
}