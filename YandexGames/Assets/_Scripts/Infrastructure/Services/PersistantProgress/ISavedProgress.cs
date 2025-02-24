using _Scripts.Data;

namespace _Scripts.Infrastructure.Services.PersistantProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void UpdateProgress(PlayerProgress progress);
    }
}