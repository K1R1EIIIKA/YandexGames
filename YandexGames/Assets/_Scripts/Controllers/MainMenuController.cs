using _Scripts.BuffLogic;
using _Scripts.Enums;
using _Scripts.Infrastructure.Core.States;
using _Scripts.Infrastructure.Inventory;
using _Scripts.YG;
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
        [SerializeField] private Button _accountButton;
        [SerializeField] private RectTransform _casesContainer;
        [SerializeField] private AccountController _accountController;

        private CaseManager _caseManager;
        private GameStateMachine _gameStateMachine;
        private InventoryController _inventoryController;
        private BuffController _buffController;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine, InventoryController inventoryController, CaseManager caseManager,
            BuffController buffController)
        {
            _gameStateMachine = gameStateMachine;
            _inventoryController = inventoryController;
            _caseManager = caseManager;
            _buffController = buffController;

            Initialize();
        }

        private void Initialize()
        {
            _shopButton.onClick.AddListener(OnShopButtonClicked);
            _charactersButton.onClick.AddListener(OnCharactersButtonClicked);
            _bedsButton.onClick.AddListener(OnBedsButtonClicked);
            _backgroundsButton.onClick.AddListener(OnBackgroundsButtonClicked);
            _accountButton.onClick.AddListener(OnAccountButtonClicked);

            InitializeCases();
        }

        private void OnEnable()
        {
            _buffController.OnBuffsChanged += InitializeCases;
        }

        private void OnDisable()
        {
            _buffController.OnBuffsChanged -= InitializeCases;
        }

        private void InitializeCases()
        {
            _caseManager.InitializeCases(_casesContainer, CaseLocationType.MainScreen);
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

        private void OnAccountButtonClicked()
        {
            _accountController.OpenAccount();
        }
    }
}