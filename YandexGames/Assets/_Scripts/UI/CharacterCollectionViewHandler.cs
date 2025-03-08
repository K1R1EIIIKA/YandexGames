using System;
using _Scripts.Controllers;
using _Scripts.Enums;
using _Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI
{
    public class CharacterCollectionViewHandler : MonoBehaviour
    {
        [Inject] ICardController _cardController;

        [SerializeField] SelectableButtonBehaviour _openCollectionButton;
        [SerializeField] SelectableButtonBehaviour _openInventoryButton;

        [SerializeField] private SortTypeButtonBehaviour _inventorySortButton;
        [SerializeField] private SortTypeButtonBehaviour _collectionSortButton;

        [SerializeField] GridLayoutGroup _gridLayoutGroup;

        private readonly InventoryCardsSortType[] _sortTypes =
            (InventoryCardsSortType[])Enum.GetValues(typeof(InventoryCardsSortType));

        private int _currentInventorySortIndex = 0;
        private int _currentCollectionSortIndex = 0;


        private void Awake()
        {
            _cardController.Construct(_gridLayoutGroup);
        }

        private void OnEnable()
        {
            _openInventoryButton.Button.onClick.AddListener(OnInventoryButtonClicked);
            _openCollectionButton.Button.onClick.AddListener(OnCollectionButtonClicked);

            _inventorySortButton.Button.onClick.AddListener(OnInventorySortButtonClicked);
            _collectionSortButton.Button.onClick.AddListener(OnCollectionSortButtonClicked);

            _currentInventorySortIndex = 0;

            OnInventoryButtonClicked();
        }

        private void OnInventorySortButtonClicked()
        {
            _currentInventorySortIndex = (_currentInventorySortIndex + 1) % _sortTypes.Length;

            switch (_currentInventorySortIndex)
            {
                case 0:
                    _inventorySortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Rarity), true);
                    _cardController.SortInventoryCards(InventoryCardsSortType.ByRareAsc);
                    break;
                case 1:
                    _inventorySortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Rarity), false);
                    _cardController.SortInventoryCards(InventoryCardsSortType.NyRareDesc);
                    break;
                case 2:
                    _inventorySortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Count), true);
                    _cardController.SortInventoryCards(InventoryCardsSortType.ByCountAsc);
                    break;
                case 3:
                    _inventorySortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Count), false);
                    _cardController.SortInventoryCards(InventoryCardsSortType.ByCountDesc);
                    break;
            }

            _cardController.ShowPlayerBeds();
        }

        private void OnCollectionSortButtonClicked()
        {
            _currentCollectionSortIndex = (_currentCollectionSortIndex + 1) % _sortTypes.Length;

            switch (_currentCollectionSortIndex)
            {
                case 0:
                    _collectionSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Availability), true);
                    _cardController.SortCollectionCards(CollectionCardsSortType.ByHasAsc);
                    break;
                case 1:
                    _collectionSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Availability), false);
                    _cardController.SortCollectionCards(CollectionCardsSortType.ByHasDesc);
                    break;
                case 2:
                    _collectionSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Rarity), true);
                    _cardController.SortCollectionCards(CollectionCardsSortType.ByRareAsc);
                    break;
                case 3:
                    _collectionSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Rarity), false);
                    _cardController.SortCollectionCards(CollectionCardsSortType.ByRareDesc);
                    break;
            }

            _cardController.ShowAllBeds();
        }


        private void OnDisable()
        {
            _openInventoryButton.Button.onClick.RemoveListener(OnInventoryButtonClicked);
            _openCollectionButton.Button.onClick.RemoveListener(OnCollectionButtonClicked);

            _inventorySortButton.Button.onClick.RemoveListener(OnInventorySortButtonClicked);
            _collectionSortButton.Button.onClick.RemoveListener(OnCollectionSortButtonClicked);
        }

        private void OnInventoryButtonClicked()
        {
            _openInventoryButton.Select();
            _openCollectionButton.Deselect();

            _collectionSortButton.gameObject.SetActive(false);
            _inventorySortButton.gameObject.SetActive(true);
            _inventorySortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Rarity), true);

            _cardController.SortInventoryCards(InventoryCardsSortType.ByRareAsc);
            _cardController.ShowPlayerBeds();
        }

        private void OnCollectionButtonClicked()
        {
            _openCollectionButton.Select();
            _openInventoryButton.Deselect();

            _inventorySortButton.gameObject.SetActive(false);
            _collectionSortButton.gameObject.SetActive(true);
            _collectionSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Availability), true);

            _cardController.SortCollectionCards(CollectionCardsSortType.ByHasAsc);
            _cardController.ShowAllBeds();
        }
    }
}