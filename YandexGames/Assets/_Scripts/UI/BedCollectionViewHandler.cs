using System;
using _Scripts.Controllers;
using _Scripts.Enums;
using _Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI
{
    public class BedCollectionViewHandler : MonoBehaviour
    {
        [Inject] BedsController _bedsController;

        [SerializeField] GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private SortTypeButtonBehaviour _bedSortButton;

        private CollectionSortType[] _sortTypes =
            (CollectionSortType[])Enum.GetValues(typeof(CollectionSortType));

        private int _currentBedSortIndex = 0;

        private void Awake()
        {
            _bedsController.Construct(_gridLayoutGroup);
        }

        private void OnEnable()
        {
            _bedsController.ShowPlayerBeds();

            _bedSortButton.Button.onClick.AddListener(OnBedSortButtonClicked);
            _currentBedSortIndex = 0;

            OnBedSortButtonClicked();
        }

        private void OnDisable()
        {
            _bedSortButton.Button.onClick.RemoveListener(OnBedSortButtonClicked);
        }

        private void OnBedSortButtonClicked()
        {
            _currentBedSortIndex = (_currentBedSortIndex + 1) % _sortTypes.Length;
            Debug.Log(_currentBedSortIndex);

            switch (_currentBedSortIndex)
            {
                case 0:
                    _bedSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Rarity), true);
                    _bedsController.SortBeds(CollectionSortType.ByRareAsc);
                    break;
                case 1:
                    _bedSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Rarity), false);
                    _bedsController.SortBeds(CollectionSortType.ByRareDesc);
                    break;
                case 2:
                    _bedSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Availability), true);
                    _bedsController.SortBeds(CollectionSortType.ByHasAsc);
                    break;
                case 3:
                    _bedSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Availability), false);
                    _bedsController.SortBeds(CollectionSortType.ByHasDesc);
                    break;
            }
        }
    }
}