using System;
using _Scripts.Controllers;
using _Scripts.Enums;
using _Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI
{
    public class BackgroundCollectionViewHandler : MonoBehaviour
    {
        [Inject] BackgroundsController _backgroundsController;

        [SerializeField] GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private SortTypeButtonBehaviour _backgroundSortButton;

        private readonly CollectionSortType[] _sortTypes =
            (CollectionSortType[])Enum.GetValues(typeof(CollectionSortType));

        private int _currentBedSortIndex = 0;

        private void Awake()
        {
            _backgroundsController.Construct(_gridLayoutGroup);
        }

        private void OnEnable()
        {
            _backgroundsController.ShowPlayerBackgrounds();

            _backgroundSortButton.Button.onClick.AddListener(OnBedSortButtonClicked);
            _currentBedSortIndex = 0;

            OnBedSortButtonClicked();
        }

        private void OnDisable()
        {
            _backgroundSortButton.Button.onClick.RemoveListener(OnBedSortButtonClicked);
        }

        private void OnBedSortButtonClicked()
        {
            _currentBedSortIndex = (_currentBedSortIndex + 1) % _sortTypes.Length;

            switch (_currentBedSortIndex)
            {
                case 0:
                    _backgroundSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Rarity), true);
                    _backgroundsController.SortBackgrounds(CollectionSortType.ByRareAsc);
                    break;
                case 1:
                    _backgroundSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Rarity), false);
                    _backgroundsController.SortBackgrounds(CollectionSortType.ByRareDesc);
                    break;
                case 2:
                    _backgroundSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Availability), true);
                    _backgroundsController.SortBackgrounds(CollectionSortType.ByHasAsc);
                    break;
                case 3:
                    _backgroundSortButton.ChangeSortType(LocalizedStrings.ConvertSortToString(SortType.Availability), false);
                    _backgroundsController.SortBackgrounds(CollectionSortType.ByHasDesc);
                    break;
            }
        }
    }
}