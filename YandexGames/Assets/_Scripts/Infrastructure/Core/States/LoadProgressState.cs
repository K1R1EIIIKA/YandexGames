using _Scripts.Data;
using _Scripts.Enums;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core.States
{
    public class LoadProgressState : IState
    {
        private GameStateMachine _gameStateMachine;
        private IPersistantProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private GameFactory _gameFactory;

        [Inject]
        public LoadProgressState(GameStateMachine gameStateMachine, IPersistantProgressService progressService,
            ISaveLoadService saveLoadService, GameFactory gameFactory)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;

            Debug.Log("Load Progress State initialized");
        }

        public void Enter()
        {
            Debug.Log("Entering LoadProgressState...");

            LoadProgressOrInitNew(); // Загружаем или создаем новый прогресс

            Debug.Log("After LoadProgressOrInitNew: " +
                      (_progressService.Progress != null ? JsonUtility.ToJson(_progressService.Progress) : "NULL"));

            _saveLoadService.SaveProgressWithoutWriting(); // Сразу пробуем сохранить
            foreach (var progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }

            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.MainScreen);
        }


        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            var loadedProgress = _saveLoadService.LoadProgress();

            if (loadedProgress != null)
            {
                _progressService.Progress = loadedProgress;
                Debug.Log("Loaded existing progress: " + JsonUtility.ToJson(loadedProgress));
            }
            else
            {
                _progressService.Progress = NewProgress();
                Debug.Log("No progress found, initializing new one");
            }
        }


        private PlayerProgress NewProgress()
        {
            Debug.Log("NEW PROGRESS");
            return new PlayerProgress(SceneNames.MainScreen);
        }
    }
}