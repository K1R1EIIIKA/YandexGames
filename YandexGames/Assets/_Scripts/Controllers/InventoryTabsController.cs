using System.Linq;
using _Scripts.Enums;
using _Scripts.Infrastructure.Inventory;
using _Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Scripts.Controllers
{
    public class InventoryTabsController : MonoBehaviour
    {
        [SerializeField] private InventoryTab[] _inventoryTabs;
        [SerializeField] private InventoryViewHandler _inventoryViewHandler;

        private TransactionController _transactionController;

        public static InventoryTabsController Instance { get; private set; }

        [Inject]
        public void Construct(TransactionController transactionController)
        {
            _transactionController = transactionController;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Initialize()
        {
        }

        public void OpenTab(InventoryTabType tabType)
        {
            foreach (var inventoryTab in _inventoryTabs)
                inventoryTab.CloseTab();

            var tab = _inventoryTabs.First(tab => tab.TabType == tabType);

            tab.OpenTab();

            switch (tabType)
            {
                case InventoryTabType.Characters:
                    tab.SetText($"{_transactionController.PlayerCards.Count}/{_transactionController.AllCards.Count}");
                    _inventoryViewHandler.SelectCharactersButton();
                    break;
                case InventoryTabType.Beds:
                    tab.SetText($"{_transactionController.PlayerBeds.Count}/{_transactionController.AllBeds.Count}");
                    _inventoryViewHandler.SelectBedsButton();
                    break;
                case InventoryTabType.Backgrounds:
                    _inventoryViewHandler.SelectBackgroundsButton();
                    tab.SetText($"{_transactionController.PlayerBackgrounds.Count}/{_transactionController.AllBackgrounds.Count}");
                    break;
            }
        }
    }
}