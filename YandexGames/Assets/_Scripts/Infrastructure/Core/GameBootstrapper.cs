using System.Collections;
using _Scripts.Infrastructure.Core.SceneTransitions;
using _Scripts.Infrastructure.Core.States;
using UnityEngine;

namespace _Scripts.Infrastructure.Core
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        private SceneLoader _sceneLoader;
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_loadingCurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}