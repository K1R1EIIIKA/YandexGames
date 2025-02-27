using System;
using _Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Infrastructure.Inventory
{
    public class InventoryTab : MonoBehaviour
    {
        [SerializeField] private InventoryTabType _inventoryTab;
        [SerializeField] private TextMeshProUGUI _itemsCount;
        
        public InventoryTabType TabType => _inventoryTab;

        public void OpenTab()
        {
            gameObject.SetActive(true);
        }

        public void SetText(string text)
        {
            _itemsCount.text = text;
        }
    }
}