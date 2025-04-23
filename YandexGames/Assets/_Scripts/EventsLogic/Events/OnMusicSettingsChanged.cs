namespace _Scripts.EventsLogic.Events
{
    public class OnMusicSettingsChanged : IEvent
    {
        public bool IsMusicEnabled { get; }

        public OnMusicSettingsChanged(bool isMusicEnabled)
        {
            IsMusicEnabled = isMusicEnabled;
        }
    }
}