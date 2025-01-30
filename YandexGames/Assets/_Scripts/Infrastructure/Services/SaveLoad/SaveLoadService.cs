using _Scripts.Data;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private IGameFactory _gameFactory;
        private IPersistantProgressService _progressService;

        [Inject]
        public void Construct(IPersistantProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }

        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            Debug.Log(_progressService.Progress.ToJson());
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }
    }
}