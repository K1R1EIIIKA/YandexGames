using _Scripts.Data;

namespace _Scripts.Infrastructure.Services.PersistantProgress
{
    public interface IPersistantProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}