using _Scripts.Data;
using _Scripts.Infrastructure.Core.SceneTransitions;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core.States
{
    public class LoadProgressState : IState
    {
        private const string FirstSceneName = "KirillScene";

        private GameStateMachine _gameStateMachine;
        private IPersistantProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public LoadProgressState(GameStateMachine gameStateMachine, IPersistantProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            Debug.Log("Load Progress State initialized");
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.LevelsProgress.SceneNameName);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            return new PlayerProgress(FirstSceneName);
        }
    }
}