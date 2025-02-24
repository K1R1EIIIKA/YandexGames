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

        [SerializeField] private Button _charactersTabButton;
        [SerializeField] private Button _bedsTabButton;
        [SerializeField] private Button _backgroundsTabButton;

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
            _charactersTabButton.onClick.AddListener(OnCharactersTabButtonClicked);
            _bedsTabButton.onClick.AddListener(OnBedsTabButtonClicked);
            _backgroundsTabButton.onClick.AddListener(OnBackgroundsTabButtonClicked);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _charactersTabButton.onClick.RemoveListener(OnCharactersTabButtonClicked);
            _bedsTabButton.onClick.RemoveListener(OnBedsTabButtonClicked);
            _backgroundsTabButton.onClick.RemoveListener(OnBackgroundsTabButtonClicked);
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
    }
}