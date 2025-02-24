using System;
using _Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Infrastructure.Inventory
{
    public class InventoryTab : MonoBehaviour
    {
        [SerializeField] private InventoryTabType _inventoryTab;
        
        public InventoryTabType TabType => _inventoryTab;

        public void OpenTab()
        {
            gameObject.SetActive(true);
        }
    }
}