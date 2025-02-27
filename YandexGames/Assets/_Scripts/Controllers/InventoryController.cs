using _Scripts.Enums;
using _Scripts.Infrastructure.Factory;
using _Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Scripts.Controllers
{
    public class InventoryController
    {
        private ICardController _cardsController;

        [Inject]
        public void Construct(ICardController cardsController)
        {
            _cardsController = cardsController;

            Debug.Log("Inventory Controller initialized");
        }

        public void Initialize()
        {
            _cardsController.Initialize();

            InventoryTabsController.Instance.Initialize();
        }

        public void OpenTab(InventoryTabType tabType) => InventoryTabsController.Instance.OpenTab(tabType);
    }
}