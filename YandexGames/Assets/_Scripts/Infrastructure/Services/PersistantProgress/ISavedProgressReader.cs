using _Scripts.Data;

namespace _Scripts.Infrastructure.Services.PersistantProgress
{
    public interface ISavedProgressReader
    {
        public void LoadProgress(PlayerProgress progress);
    }
}