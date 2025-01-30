using _Scripts.Data;

namespace _Scripts.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PlayerProgress LoadProgress();

        void SaveProgress();
    }
}