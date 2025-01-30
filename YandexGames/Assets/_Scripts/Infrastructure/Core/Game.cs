using _Scripts.Infrastructure.Core.SceneTransitions;
using _Scripts.Infrastructure.Core.States;
using UnityEngine;

namespace _Scripts.Infrastructure.Core
{
    public class Game : MonoBehaviour
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain);
        }
    }
}