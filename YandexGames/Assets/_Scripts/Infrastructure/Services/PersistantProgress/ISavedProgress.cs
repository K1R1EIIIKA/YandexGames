using _Scripts.Data;
using _Scripts.View;
using UnityEngine.UI;

namespace _Scripts.Infrastructure.Services.PersistantProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void UpdateProgress(PlayerProgress progress);
    }
}