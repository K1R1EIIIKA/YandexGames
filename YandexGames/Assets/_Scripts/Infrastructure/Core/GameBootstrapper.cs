using System.Collections;
using _Scripts.Infrastructure.Core.SceneTransitions;
using _Scripts.Infrastructure.Core.States;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = ProjectContext.Instance.Container.Instantiate<Game>();

            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
            Debug.Log("Bootstrapper made his deal");
        }
    }
}