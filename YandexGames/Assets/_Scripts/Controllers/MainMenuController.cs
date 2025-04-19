using System.Collections;
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

        [Header("Controllers")]
        [SerializeField] private AccountController _accountController;
        [SerializeField] private SettingsController _settingsController;

        private CaseManager _caseManager;
        private GameStateMachine _gameStateMachine;
        private InventoryController _inventoryController;
        private BuffController _buffController;

        [Header("Random Button")] [SerializeField]
        private float _minInterval = 5f;

        [SerializeField] private float _maxInterval = 10f;
        [SerializeField] private AdButton _randomButton;

        private Coroutine _randomButtonCoroutine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine, InventoryController inventoryController,
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

        private void OnSettingsButtonClicked()
        {
            _settingsController.OpenSettings();
        }

        private void OnEnable()
        {
            _buffController.OnBuffsChanged += InitializeCases;

            _randomButton.InstantHide();
            _randomButtonCoroutine = StartCoroutine(RandomButtonCoroutine());
        }

        private IEnumerator RandomButtonCoroutine()
        {
            while (true)
            {
                float waitTime = Random.Range(_minInterval, _maxInterval);
                yield return new WaitForSeconds(waitTime);

                _randomButton.Show();

                yield return new WaitForSeconds(5f);

                _randomButton.Hide();
            }
        }

        private void OnDisable()
        {
            _buffController.OnBuffsChanged -= InitializeCases;

            if (_randomButtonCoroutine != null)
            {
                StopCoroutine(_randomButtonCoroutine);
            }
        }

        private void InitializeCases()
        {
            _caseManager.InitializeCases(_casesContainer, CaseLocationType.MainScreen);
            _caseManager.InitializeAdCases(_adCasesContainer);
            _caseManager.InitializeLimitedCase(_limitedCaseContainer);
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