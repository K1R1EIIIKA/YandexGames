using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Data;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.Tools;
using _Scripts.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controller
{
    public class InventoryController : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private CardView _cardViewPrefab;

        private List<GameObject> _cards = new List<GameObject>();
        private IGameFactory _gameFactory;

        private List<CardData> _playerCardsData = new List<CardData>();
        private List<CardData> _allCardsSet = new List<CardData>();
        [Inject] private ISaveLoadService _saveLoadService;

        [Header("Test")] [SerializeField] List<Sprite> cardSprites;
        [SerializeField] private Button _collectionViewActivateButton;
        private const string CARDS_CONFIGURATION_FILE_NAME = "CardsConfig";

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Awake()
        {
            _gameFactory.Register(this);
            _collectionViewActivateButton.onClick.AddListener(ShowAllCards);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.LevelsProgress.PlayerCards = _playerCardsData;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _playerCardsData = progress.LevelsProgress.PlayerCards;
            _allCardsSet = CardCSVHandler.ReadCSV(CARDS_CONFIGURATION_FILE_NAME);

            TestInventoryFilling();
            CheckConfigOnMistakes();

            ShowPlayerCards();
        }

        private void TestInventoryFilling()
        {
            if (_playerCardsData.Count == 0)
            {
                foreach (Sprite cardSprite in cardSprites)
                {
                    GameObject cardObject = _gameFactory.CreateObjectCard();
                    CardView cardView = cardObject.GetComponent<CardView>();
                    cardView.Initialize(Color.cyan, cardSprite, cardSprite.name);

                    CardData cardData = new CardData(_playerCardsData.Count.ToString(), cardSprite.name,
                        cardSprite.name, 15, Rare.Advertisement, true);

                    _cards.Add(cardObject);
                    _playerCardsData.Add(cardData);
                    cardObject.transform.SetParent(_gridLayout.transform);
                }

                _saveLoadService.SaveProgress();
            }
        }

        private void CheckConfigOnMistakes()
        {
            List<string> playersUniqueCardNames = _playerCardsData.Select(x => x.ImageName).Distinct().ToList();

            foreach (string playersUniqueCardName in playersUniqueCardNames)
            {
                CardData cardData = _allCardsSet.FirstOrDefault(x => x.ImageName == playersUniqueCardName);

                if (cardData != null)
                {
                    cardData.IsOpen = true;
                }
            }
        }

        private void ShowPlayerCards()
        {
            ClearCards();

            CreateCardsView(_playerCardsData);
        }

        private void ShowAllCards()
        {
            ClearCards();

            CreateCardsView(_allCardsSet);
        }

        private void CreateCardsView(List<CardData> cardsSet)
        {
            foreach (CardData cardData in cardsSet)
            {
                GameObject card = CreateCardView(cardData);
                _cards.Add(card);
                card.transform.SetParent(_gridLayout.transform);
            }
        }

        private GameObject CreateCardView(CardData cardData)
        {
            GameObject cardObject = _gameFactory.CreateObjectCard();
            CardView cardView = cardObject.GetComponent<CardView>();

            cardView.Initialize(cardData.Rare.ToHexColor().ToColor(), CardSpritesLibrary.LoadSprite(cardData.ImageName),
                cardData.Name);

            if (!cardData.IsOpen)
            {
                cardView.SetViewToClosed();
            }

            return cardObject;
        }

        private void ClearCards()
        {
            if (_cards == null) return;

            foreach (GameObject card in _cards)
            {
                Destroy(card);
            }

            _cards.Clear();
        }
    }
}