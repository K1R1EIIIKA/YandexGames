using _Scripts.Enums;
using _Scripts.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace _Scripts.Controllers
{
    public class InventoryController
    {
        private ICardController _cardsController;
        private IGameFactory _gameFactory;

        [Inject]
        public void Construct(ICardController cardsController, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _cardsController = cardsController;

            Debug.Log("Inventory Controller initialized");
        }

        public void Initialize()
        {
            _cardsController.Initialize();

            _gameFactory.Register(_cardsController.GetSavedProgress());
            InventoryTabsController.Instance.Initialize();
        }

        public void OpenTab(InventoryTabType tabType) => InventoryTabsController.Instance.OpenTab(tabType);
    }
}