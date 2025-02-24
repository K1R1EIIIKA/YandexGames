using System;
using System.Collections;
using System.Linq;
using _Scripts.Infrastructure.Core.SceneTransitions;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
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

        [Inject]
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            GameFactory gameFactory, IPersistantProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;

            Debug.Log("LoadLevelState was initialized");
        }

        public async void Enter(string payload, Action onLoad)
        {
            _loadingCurtain.Show();
            _gameFactory.CleanUp();
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