using _Scripts.Data;
using _Scripts.Infrastructure.Core.SceneTransitions;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Scripts.Infrastructure.Core.States
{
    public class LoadProgressState : IState
    {
        private const string FirstSceneName = "GameScene";

        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistantProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;

        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            Debug.Log("Switch to next scene");
            // _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.TowerData.SceneName);
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