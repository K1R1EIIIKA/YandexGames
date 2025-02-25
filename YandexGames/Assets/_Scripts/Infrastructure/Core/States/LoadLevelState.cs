using System;
using System.Collections;
using System.Linq;
using _Scripts.Infrastructure.Core.SceneTransitions;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        private GameFactory _gameFactory;
        private IPersistantProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            GameFactory gameFactory, IPersistantProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;

            Debug.Log("LoadLevelState was initialized");
        }

        public async void Enter(string payload, Action onLoad)
        {
            // _saveLoadService.SaveProgress();

            _loadingCurtain.Show();
            _gameFactory.CleanDublicates();
            await _sceneLoader.SwitchSceneWithUnload(payload);
            OnLoadComplete();

            onLoad?.Invoke();
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoadComplete()
        {
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }

            Debug.Log("Informing progress readers");
        }

        private void InitGameWorld()
        {
            //Object creating and initializing (progress + gamefactory)
        }
    }
}