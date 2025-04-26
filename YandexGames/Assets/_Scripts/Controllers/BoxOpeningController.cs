using System.Collections.Generic;
using _Scripts.Data.Cards;
using _Scripts.Enums;
using _Scripts.Infrastructure.Core.States;
using _Scripts.ScriptableObjects;
using _Scripts.Sound;
using _Scripts.Tools;
using _Scripts.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class BoxOpeningController : MonoBehaviour
    {
        [SerializeField] private Button _openBoxButton;
        [SerializeField] private Button _otherBoxButton;
        [SerializeField] private Button _charBoxButton;
        [SerializeField] private Button _moneyBoxButton;

        [Header("Containers")] [SerializeField]
        private GameObject _caseContainer;

        [SerializeField] private GameObject _moneyContainer;
        [SerializeField] private GameObject _cardContainer;

        [Header("UI")] [SerializeField] private Image _caseImage;
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

        private float _moneyLoot;
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
            _openBoxButton.onClick.AddListener(HandleBoxClick);
            _otherBoxButton.onClick.AddListener(HandleBoxClick);
            _charBoxButton.onClick.AddListener(HandleBoxClick);
            _moneyBoxButton.onClick.AddListener(HandleBoxClick);
        }

        private void OnDisable()
        {
            _openBoxButton.onClick.RemoveListener(HandleBoxClick);
            _otherBoxButton.onClick.RemoveListener(HandleBoxClick);
            _charBoxButton.onClick.RemoveListener(HandleBoxClick);
            _moneyBoxButton.onClick.RemoveListener(HandleBoxClick);
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
            switch (_caseData)
            {
                case MoneyCaseData moneyCaseData:
                    _transactionController.AddMoneyCase(moneyCaseData);
                    break;
                case LimitedCaseData limitedCaseData:
                    _transactionController.AddLimitedCase(limitedCaseData);
                    break;
            }

            if (_caseData == null) Debug.LogError("CaseData is null");

            _cardsLoot.Clear();
            _backgroundChanger.ChangeBackground(BackgroundColor.Main);

            SetContainer(ContainerType.Money);
            _moneyLoot = _caseData.GetRandomCoins();
            _transactionController.AddMoney(_moneyLoot);

            for (int i = 0; i < _itemsCountValue - 1; i++)
            {
                var card = _caseData.GetRandomCard();
                card.IsOpen = true;
                _cardsLoot.Add(card);
            }

            _transactionController.AddCaseCounter();
            _transactionController.AddPlayerCards(_cardsLoot);

            if ((int)_caseData.Tier > (int)_transactionController.CurrentCaseTier)
            {
                _transactionController.SetCaseTier(_caseData.Tier);
            }

            SortCards();

            ShowMoneyLoot();
        }

        private void ShowMoneyLoot()
        {
            SetContainer(ContainerType.Money);
            _openBoxButton.interactable = false;
            _otherBoxButton.interactable = false;
            _moneyBoxButton.interactable = false;
            _charBoxButton.interactable = false;

            // 2) Инициализация: уменьшаем масштаб до 0
            _moneyImage.transform.localScale = Vector3.zero;
            _moneyLootText.transform.localScale = Vector3.zero;

            // 3) Прокидываем анимацию “вылета” (масштабирование с эффектом “отскока”)
            _moneyImage.transform
                .DOScale(Vector3.one, 0.2f)
                .SetEase(Ease.OutBack);
            _moneyLootText.transform
                .DOScale(Vector3.one, 0.2f)
                .SetEase(Ease.OutBack)
                .SetDelay(0.1f).OnComplete(() =>
                {
                    _openBoxButton.interactable = true;
                    _otherBoxButton.interactable = true;
                    _moneyBoxButton.interactable = true;
                    _charBoxButton.interactable = true;
                });

            // остальной код
            _moneyLootText.text = BigNumberFormatter.FormatBigNumber(_moneyLoot);
            _remainItemsCountValue--;
            _itemsCount.text = _remainItemsCountValue.ToString();
            AudioController.Instance.PlaySound(SoundName.Yes);
        }

        private void ShowCardLoot()
        {
            SetContainer(ContainerType.Card);

            // Блокируем кнопку, чтобы не было повторных кликов
            _openBoxButton.interactable = false;
            _otherBoxButton.interactable = false;
            _moneyBoxButton.interactable = false;
            _charBoxButton.interactable = false;

            // Останавливаем предыдущие твины на контейнере и его Transform
            _cardContainer.transform.DOKill();

            var card = _cardsLoot[0];
            _cardImage.sprite = card.Image;
            _cardNameText.text = card.GetName();
            _cardRareText.text = LocalizedStrings.ConvertRarityToString(card.Rarity);

            // Сохраняем стартовую позицию, чтобы всегда корректно сбрасывать
            Vector3 startPos = new Vector3(0, -31f, 0);
            Vector3 raisedPos = new Vector3(0, 131f, 0);

            _cardContainer.transform.localScale = Vector3.zero;
            _cardContainer.transform.localPosition = startPos;
            switch (card.Rarity)
            {
                case Rarity.Common:
                    _backgroundChanger.ChangeBackground(BackgroundColor.Common);
                    AudioController.Instance.PlaySound(SoundName.Yes);
                    break;
                case Rarity.Rare:
                    _backgroundChanger.ChangeBackground(BackgroundColor.Rare);
                    AudioController.Instance.PlaySound(SoundName.Yes);
                    break;
                case Rarity.SuperRare:
                    _backgroundChanger.ChangeBackground(BackgroundColor.SuperRare);
                    AudioController.Instance.PlaySound(SoundName.Yes);
                    break;
                case Rarity.SuperMegaRare:
                    _backgroundChanger.ChangeBackground(BackgroundColor.SuperMegaRare);
                    AudioController.Instance.PlaySound(SoundName.GetLegendary);
                    break;
                case Rarity.Special:
                    _backgroundChanger.ChangeBackground(BackgroundColor.Special);
                    AudioController.Instance.PlaySound(SoundName.GetLegendary);
                    break;
            }


            // Собираем новую последовательность
            Sequence seq = DOTween.Sequence()
                .Append(_cardContainer.transform
                    .DOScale(1f, 0.3f)
                    .SetEase(Ease.OutBack))
                .Join(_cardContainer.transform
                    .DOLocalMove(raisedPos, 0.3f)
                    .SetEase(Ease.OutBack))
                .OnComplete(() =>
                {
                    // Фон и звук после «вылета»

                    // Разблокируем кнопку — теперь можно следующий клик
                    _openBoxButton.interactable = true;
                    _otherBoxButton.interactable = true;
                    _moneyBoxButton.interactable = true;
                    _charBoxButton.interactable = true;
                });

            _remainItemsCountValue--;
            _itemsCount.text = _remainItemsCountValue.ToString();
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