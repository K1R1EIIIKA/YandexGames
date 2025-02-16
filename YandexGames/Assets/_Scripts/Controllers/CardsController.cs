using System.Collections.Generic;
using System.Linq;
using _Scripts.Data;
using _Scripts.Data.Cards;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.Tools;
using _Scripts.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class CardsController : ISavedProgress, ICardController, IInitializable
    {
        private const string CARDS_CONFIGURATION_FILE_NAME = "CardsConfig";

        private List<CardData> _allCardsSet = new List<CardData>();
        private List<CardData> _playerCardsData = new List<CardData>();

        private List<GameObject> _cards = new List<GameObject>();

        private IGameFactory _gameFactory;
        private ISaveLoadService _saveLoadService;

        private GridLayoutGroup _gridLayout;

        [Inject]
        public CardsController(ISaveLoadService saveLoadService, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _saveLoadService = saveLoadService;

            _gameFactory.Register(this);
            Debug.Log("Card Controller Initialized");
        }

        public ISavedProgress GetSavedProgress()
        {
            return this;
        }

        public void Construct(GridLayoutGroup gridLayout)
        {
            _gridLayout = gridLayout;
        }

        public void Initialize()
        {
        }

        public void ShowPlayerCards()
        {
            ClearCards();

            CreateCardsView(_playerCardsData);
        }

        public void ShowAllCards()
        {
            ClearCards();

            CreateCardsView(_allCardsSet);
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
                for (int i = 0; i < 3; i++)
                {
                    GameObject cardObject = _gameFactory.CreateObjectCard();
                    CardView cardView = cardObject.GetComponent<CardView>();

                    CardData cardData = new CardData(_playerCardsData.Count.ToString(), _allCardsSet[i].Name,
                        _allCardsSet[i].ImageName, _allCardsSet[i].Cost, _allCardsSet[i].Rare, true);

                    cardView.Initialize(Color.cyan, CardSpritesLibrary.LoadSprite(cardData.ImageName), cardData.Name);

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
                Object.Destroy(card);
            }

            _cards.Clear();
        }
    }
}