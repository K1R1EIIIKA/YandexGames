using System.Linq;
using _Scripts.Enums;
using _Scripts.Infrastructure.Inventory;
using UnityEngine;
using Zenject;

namespace _Scripts.Controllers
{
    public class InventoryTabsController : MonoBehaviour
    {
        [SerializeField] private InventoryTab[] _inventoryTabs;

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
            {
                inventoryTab.gameObject.SetActive(false);
            }

            var tab = _inventoryTabs.First(tab => tab.TabType == tabType);

            tab.OpenTab();

            switch (tabType)
            {
                case InventoryTabType.Characters:
                    tab.SetText($"{_transactionController.PlayerCards.Count}/{_transactionController.AllCards.Count}");
                    break;
                case InventoryTabType.Beds:
                    tab.SetText($"0");
                    break;
                case InventoryTabType.Backgrounds:
                    tab.SetText($"0");
                    break;
            }
        }
    }
}