using System;
using _Scripts.Controllers;
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
        
        [SerializeField] GridLayoutGroup _gridLayoutGroup;

        private void Awake()
        {
            _cardController.Construct(_gridLayoutGroup);
        }

        private void OnEnable()
        {
            _openInventoryButton.Button.onClick.AddListener(OnInventoryButtonClicked);
            _openCollectionButton.Button.onClick.AddListener(OnCollectionButtonClicked);

            OnInventoryButtonClicked();
        }

        private void OnDisable()
        {
            _openInventoryButton.Button.onClick.RemoveListener(OnInventoryButtonClicked);
            _openCollectionButton.Button.onClick.RemoveListener(OnCollectionButtonClicked);
        }

        private void OnInventoryButtonClicked()
        {
            _cardController.ShowPlayerCards();

            _openInventoryButton.Select();
            _openCollectionButton.Deselect();
        }

        private void OnCollectionButtonClicked()
        {
            _cardController.ShowAllCards();

            _openCollectionButton.Select();
            _openInventoryButton.Deselect();
        }
    }
}