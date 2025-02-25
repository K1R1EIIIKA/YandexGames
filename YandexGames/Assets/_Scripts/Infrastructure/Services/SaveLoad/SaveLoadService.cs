using System.Linq;
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
            Debug.Log(PlayerPrefs.GetString(ProgressKey));
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }

        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters.Where(writer => writer != null))
                progressWriter.UpdateProgress(_progressService.Progress);
            Debug.Log(string.Join(",", _gameFactory.ProgressWriters) + " progress writers updated progress");

            Debug.Log($"Before actual saving: {_progressService.Progress != null}, data: " +
                      (_progressService.Progress != null ? JsonUtility.ToJson(_progressService.Progress) : "NULL"));

            if (_progressService.Progress == null)
            {
                Debug.LogError("Attempted to save NULL progress! Skipping save...");
                return;
            }

            string progressJson = JsonUtility.ToJson(_progressService.Progress);
            PlayerPrefs.SetString(ProgressKey, progressJson);
            PlayerPrefs.Save();
            Debug.Log("Progress saved successfully: " + progressJson);
        }

        public void SaveProgressWithoutWriting()
        {
            string progressJson = JsonUtility.ToJson(_progressService.Progress);
            PlayerPrefs.SetString(ProgressKey, progressJson);
            PlayerPrefs.Save();
            Debug.Log("Progress saved successfully: " + progressJson);
        }
    }
}