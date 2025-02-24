using System.Linq;
using _Scripts.Enums;
using _Scripts.Infrastructure.Inventory;
using UnityEngine;

namespace _Scripts.Controllers
{
    public class InventoryTabsController : MonoBehaviour
    {
        [SerializeField] private InventoryTab[] _inventoryTabs;

        public static InventoryTabsController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject); // Удаляем дубликаты, если они вдруг появились
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

            _inventoryTabs.First(tab => tab.TabType == tabType).gameObject.SetActive(true);
        }
    }
}