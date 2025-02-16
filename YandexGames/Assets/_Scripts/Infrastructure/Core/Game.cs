using _Scripts.Infrastructure.Core.States;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core
{
    public class Game
    {
        public GameStateMachine StateMachine;
        private DiContainer _container;

        [Inject]
        public void Construct(GameStateMachine stateMachine, DiContainer container)
        {
            StateMachine = stateMachine;
            _container = container;
            Debug.Log("Game was initalized");
        }
    }
}