using System;
using _Scripts.BuffLogic;
using _Scripts.BuffLogic.Base;
using _Scripts.BuffLogic.Buffs;
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
        [SerializeField] private RectTransform _casesContainer;

        private CaseManager _caseManager;
        private GameStateMachine _gameStateMachine;
        private InventoryController _inventoryController;
        private BuffController _buffController;
        private AdRewardController _adRewardController;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine, InventoryController inventoryController, CaseManager caseManager,
            BuffController buffController, AdRewardController adRewardController)
        {
            _gameStateMachine = gameStateMachine;
            _inventoryController = inventoryController;
            _caseManager = caseManager;
            _buffController = buffController;
            _adRewardController = adRewardController;

            Initialize();
        }

        private void Initialize()
        {
            _shopButton.onClick.AddListener(OnShopButtonClicked);
            _charactersButton.onClick.AddListener(OnCharactersButtonClicked);
            _bedsButton.onClick.AddListener(OnBedsButtonClicked);
            _backgroundsButton.onClick.AddListener(OnBackgroundsButtonClicked);

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _buffController.AddBuff(new TemporaryBuff(_buffController, new DiscountBuff(10), 2f));
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
    }
}