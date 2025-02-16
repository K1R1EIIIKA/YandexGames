using _Scripts.Enums;
using _Scripts.Infrastructure.Core.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _charactersButton;
        [SerializeField] private Button _bedsButton;
        [SerializeField] private Button _backgroundsButton;

        private GameStateMachine _gameStateMachine;
        private InventoryController _inventoryController;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine, InventoryController inventoryController)
        {
            _gameStateMachine = gameStateMachine;
            _inventoryController = inventoryController;

            Initialize();
        }

        private void Initialize()
        {
            _shopButton.onClick.AddListener(OnShopButtonClicked);
            _charactersButton.onClick.AddListener(OnCharactersButtonClicked);
            _bedsButton.onClick.AddListener(OnBedsButtonClicked);
            _backgroundsButton.onClick.AddListener(OnBackgroundsButtonClicked);
        }

        private void OnShopButtonClicked()
        {
            Debug.Log("Shop button clicked");
        }

        private void OnCharactersButtonClicked()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.Inventory,
                () => _inventoryController.OpenTab(InventoryTabType.Characters));
        }

        private void OnBedsButtonClicked()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.Inventory,
                () => _inventoryController.OpenTab(InventoryTabType.Beds));
        }

        private void OnBackgroundsButtonClicked()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.Inventory,
                () => _inventoryController.OpenTab(InventoryTabType.Backgrounds));
        }
    }
}