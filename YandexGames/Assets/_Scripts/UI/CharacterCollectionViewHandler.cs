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

        [SerializeField] Button _openCollectionButton;
        [SerializeField] Button _openInventoryButton;
        
        [SerializeField] GridLayoutGroup _gridLayoutGroup;

        private void Awake()
        {
            _cardController.Construct(_gridLayoutGroup);
        }

        private void OnEnable()
        {
            _openInventoryButton.onClick.AddListener(_cardController.ShowPlayerCards);
            _openCollectionButton.onClick.AddListener(_cardController.ShowAllCards);
        }

        private void OnDisable()
        {
            _openInventoryButton.onClick.RemoveListener(_cardController.ShowPlayerCards);
            _openCollectionButton.onClick.RemoveListener(_cardController.ShowAllCards);
        }
    }
}