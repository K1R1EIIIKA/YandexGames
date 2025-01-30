using _Scripts.Data;

namespace _Scripts.Infrastructure.Services.PersistantProgress
{
    public class ProgressService : IPersistantProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}