using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Data.Cards;
using _Scripts.Data.Cases;
using _Scripts.Enums;
using _Scripts.Infrastructure.Core.States;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Tools;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class BoxOpeningController : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private GameObject _caseContainer;
        [SerializeField] private GameObject _moneyContainer;
        [SerializeField] private GameObject _cardContainer;

        [Header("UI")]
        [SerializeField] private Image _caseImage;
        [SerializeField] private Image _moneyImage;
        [SerializeField] private Image _cardImage;

        [SerializeField] private TextMeshProUGUI _moneyLootText;
        [SerializeField] private TextMeshProUGUI _cardNameText;
        [SerializeField] private TextMeshProUGUI _cardRareText;
        [SerializeField] private TextMeshProUGUI _itemsCount;

        [SerializeField] private BoxBackgroundChanger _backgroundChanger;

        private CaseData _caseData;
        private int _itemsCountValue;
        private int _remainItemsCountValue;

        private int _moneyLoot;
        private List<CardData> _cardsLoot = new();

        private GameStateMachine _gameStateMachine;
        private TransactionController _transactionController;

        public static BoxOpeningController Instance { get; private set; }

        [Inject]
        public void Construct(GameStateMachine gameStateMachine, TransactionController transactionController)
        {
            _gameStateMachine = gameStateMachine;
            _transactionController = transactionController;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            InputManager.OnMouseClick += HandleBoxClick;
        }

        private void OnDisable()
        {
            InputManager.OnMouseClick -= HandleBoxClick;
        }

        public void Initialize(CaseData caseData)
        {
            _caseData = caseData;

            SetContainer(ContainerType.Case);

            _caseImage.sprite = _caseData.CaseImage;
            _itemsCountValue = caseData.GetRandomLootCount();
            _remainItemsCountValue = _itemsCountValue;
            _itemsCount.text = _itemsCountValue.ToString();
        }

        private void HandleBoxClick()
        {
            if (_remainItemsCountValue == _itemsCountValue)
            {
                OpenBox();
            }
            else if (_remainItemsCountValue > 0)
            {
                ShowCardLoot();
            }
            else
            {
                CloseBox();
            }
        }

        private void OpenBox()
        {
            _cardsLoot.Clear();
            _backgroundChanger.ChangeBackground(BackgroundColor.Main);

            SetContainer(ContainerType.Money);
            _moneyLoot = _caseData.GetRandomCoins();
            _transactionController.AddMoney(_moneyLoot);

            for (int i = 0; i < _itemsCountValue-1; i++)
            {
                var card = _caseData.GetRandomCard();
                card.IsOpen = true;
                _cardsLoot.Add(card);
            }
            _transactionController.AddPlayerCards(_cardsLoot);

            SortCards();

            ShowMoneyLoot();
        }

        private void ShowMoneyLoot()
        {
            _moneyLootText.text = _moneyLoot.ToString();
            _remainItemsCountValue--;
            _itemsCount.text = _remainItemsCountValue.ToString();
        }

        private void ShowCardLoot()
        {
            SetContainer(ContainerType.Card);

            var card = _cardsLoot[0];

            _cardImage.sprite = card.Image;
            _cardNameText.text = card.Name;
            _cardRareText.text = card.Rarity.ToColorName();
            _remainItemsCountValue--;
            _itemsCount.text = _remainItemsCountValue.ToString();

            switch (card.Rarity)
            {
                case Rarity.Common:
                    _backgroundChanger.ChangeBackground(BackgroundColor.Common);
                    break;
                case Rarity.Rare:
                    _backgroundChanger.ChangeBackground(BackgroundColor.Rare);
                    break;
                case Rarity.SuperRare:
                    _backgroundChanger.ChangeBackground(BackgroundColor.SuperRare);
                    break;
                case Rarity.SuperMegaRare:
                    _backgroundChanger.ChangeBackground(BackgroundColor.SuperMegaRare);
                    break;
                case Rarity.Special:
                    _backgroundChanger.ChangeBackground(BackgroundColor.Special);
                    break;
            }

            _cardsLoot.RemoveAt(0);
        }

        private void CloseBox()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.MainScreen);
        }

        private void SortCards()
        {
            _cardsLoot.Sort((a, b) => a.Rarity.CompareTo(b.Rarity));
        }

        private void SetContainer(ContainerType containerType)
        {
            _caseContainer.SetActive(containerType == ContainerType.Case);
            _moneyContainer.SetActive(containerType == ContainerType.Money);
            _cardContainer.SetActive(containerType == ContainerType.Card);
        }

        private enum ContainerType
        {
            Case,
            Money,
            Card
        }
    }
}