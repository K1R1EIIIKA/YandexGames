using _Scripts.Controllers;
using _Scripts.Infrastructure.Core.States;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;
        private InventoryController _inventoryController;

        [Inject]
        public void Construct(InventoryController inventoryController)
        {
            _inventoryController = inventoryController;
        }

        private void Awake()
        {
            _game = ProjectContext.Instance.Container.Instantiate<Game>();

            _game.StateMachine.Enter<BootstrapState>();
            // _inventoryController.Initialize();

            DontDestroyOnLoad(this);
            Debug.Log("Bootstrapper made his deal");
        }
    }
}