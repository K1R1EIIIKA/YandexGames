using System.Collections;
using _Scripts.BuffLogic;
using _Scripts.Enums;
using _Scripts.EventsLogic;
using _Scripts.EventsLogic.Events;
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
        [Header("Buttons")]
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _charactersButton;
        [SerializeField] private Button _bedsButton;
        [SerializeField] private Button _backgroundsButton;
        [SerializeField] private Button _accountButton;
        [SerializeField] private Button _settingsButton;

        [Header("Case Containers")]
        [SerializeField] private RectTransform _casesContainer;
        [SerializeField] private RectTransform _adCasesContainer;
        [SerializeField] private RectTransform _limitedCaseContainer;
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Controllers")]
        [SerializeField] private AccountController _accountController;
        [SerializeField] private SettingsController _settingsController;
        [SerializeField] private NewGameController _newGameController;

        private GameStateMachine _gameStateMachine;
        private InventoryController _inventoryController;
        private CaseManager _caseManager;
        private BuffController _buffController;

        [Header("Random Button")]
        [SerializeField] private AdButton _randomButton;

        [Inject] private TransactionController _transactionController;

        [Inject]
        public void Construct(
            GameStateMachine gameStateMachine,
            InventoryController inventoryController,
            CaseManager caseManager,
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
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);

            InitializeCases();
        }

        private void OnEnable()
        {
            _buffController.OnBuffsChanged += InitializeCases;
            EventBus<OnTransactionsLoadedEvent>.OnEvent += OnTransactionsLoaded;

            if (RandomButtonService.Instance != null)
            {
                RandomButtonService.Instance.OnShow += ShowRandomButton;
                RandomButtonService.Instance.OnHide += HideRandomButton;
            }

            StartCoroutine(RestoreScrollPositionNextFrame());
        }

        private IEnumerator RestoreScrollPositionNextFrame()
        {
            yield return null;

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollRect.content);

            _scrollRect.horizontalNormalizedPosition = _transactionController.ScrollPosition;
        }

        private void Start()
        {
            if (RandomButtonService.Instance.IsShow)
                ShowRandomButton();
            else
                _randomButton.InstantHide();
        }

        private void OnDisable()
        {
            _buffController.OnBuffsChanged -= InitializeCases;
            EventBus<OnTransactionsLoadedEvent>.OnEvent -= OnTransactionsLoaded;

            if (RandomButtonService.Instance != null)
            {
                RandomButtonService.Instance.OnShow -= ShowRandomButton;
                RandomButtonService.Instance.OnHide -= HideRandomButton;
            }

            _transactionController.ScrollPosition = _scrollRect.horizontalNormalizedPosition;
        }

        private void OnTransactionsLoaded(OnTransactionsLoadedEvent @event)
        {
            if (_transactionController.IsFirstGameStarted)
            {
                // _newGameController.ShowNewGame();
                _transactionController.SetFirstGameStarted(false);
            }
        }

        private void ShowRandomButton()
        {
            _randomButton.Show();
        }

        private void HideRandomButton()
        {
            _randomButton.Hide();
        }

        private void InitializeCases()
        {
            _caseManager.InitializeCases(_casesContainer, CaseLocationType.MainScreen);
            _caseManager.InitializeAdCases(_adCasesContainer);
            // _caseManager.InitializeLimitedCase(_limitedCaseContainer);
        }

        private void OnShopButtonClicked() => Debug.Log("Shop button clicked");

        private void OnCharactersButtonClicked() => _gameStateMachine
            .Enter<LoadLevelState, string>(SceneNames.Inventory, () =>
                _inventoryController.OpenTab(InventoryTabType.Characters));

        private void OnBedsButtonClicked() => _gameStateMachine
            .Enter<LoadLevelState, string>(SceneNames.Inventory, () =>
                _inventoryController.OpenTab(InventoryTabType.Beds));

        private void OnBackgroundsButtonClicked() => _gameStateMachine
            .Enter<LoadLevelState, string>(SceneNames.Inventory, () =>
                _inventoryController.OpenTab(InventoryTabType.Backgrounds));

        private void OnAccountButtonClicked() => _accountController.OpenAccount();

        private void OnSettingsButtonClicked() => _settingsController.OpenSettings();
    }
}