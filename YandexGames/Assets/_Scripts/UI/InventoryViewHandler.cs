using System;
using _Scripts.Controllers;
using _Scripts.Enums;
using _Scripts.Infrastructure.Core.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI
{
    public class InventoryViewHandler : MonoBehaviour
    {
        [SerializeField] private Button _mainMenuButton;

        [SerializeField] private SelectableButtonBehaviour _charactersTabButton;
        [SerializeField] private SelectableButtonBehaviour _bedsTabButton;
        [SerializeField] private SelectableButtonBehaviour _backgroundsTabButton;

        private InventoryController _inventoryController;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(InventoryController inventoryController, GameStateMachine gameStateMachine)
        {
            _inventoryController = inventoryController;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _inventoryController.Initialize();
        }

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _charactersTabButton.Button.onClick.AddListener(OnCharactersTabButtonClicked);
            _bedsTabButton.Button.onClick.AddListener(OnBedsTabButtonClicked);
            _backgroundsTabButton.Button.onClick.AddListener(OnBackgroundsTabButtonClicked);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _charactersTabButton.Button.onClick.RemoveListener(OnCharactersTabButtonClicked);
            _bedsTabButton.Button.onClick.RemoveListener(OnBedsTabButtonClicked);
            _backgroundsTabButton.Button.onClick.RemoveListener(OnBackgroundsTabButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.MainScreen);
        }

        private void OnCharactersTabButtonClicked()
        {
            _inventoryController.OpenTab(InventoryTabType.Characters);
        }

        private void OnBedsTabButtonClicked()
        {
            _inventoryController.OpenTab(InventoryTabType.Beds);
        }

        private void OnBackgroundsTabButtonClicked()
        {
            _inventoryController.OpenTab(InventoryTabType.Backgrounds);
        }

        public void SelectCharactersButton()
        {
            _charactersTabButton.Select();
            _bedsTabButton.Deselect();
            _backgroundsTabButton.Deselect();
        }

        public void SelectBedsButton()
        {
            _charactersTabButton.Deselect();
            _bedsTabButton.Select();
            _backgroundsTabButton.Deselect();
        }

        public void SelectBackgroundsButton()
        {
            _charactersTabButton.Deselect();
            _bedsTabButton.Deselect();
            _backgroundsTabButton.Select();
        }
    }
}