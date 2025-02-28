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
        private const string CardsFolder = "Cards";

        private List<CardData> _allCardsSet = new List<CardData>();
        private List<PlayerCardData> _playerCardsData = new List<PlayerCardData>();

        private List<GameObject> _cards = new List<GameObject>();

        private IGameFactory _gameFactory;
        private TransactionController _transactionController;
        private ISaveLoadService _saveLoadService;

        private GridLayoutGroup _gridLayout;

        private PlayerCardData _selectedCard;

        [Inject]
        public CardsController(ISaveLoadService saveLoadService, IGameFactory gameFactory, TransactionController transactionController)
        {
            _gameFactory = gameFactory;
            _saveLoadService = saveLoadService;
            _transactionController = transactionController;

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

            CreatePlayerCardsView(_playerCardsData);
        }

        public void ShowAllCards()
        {
            ClearCards();

            CreateCardsView(_allCardsSet);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            // progress.LevelsProgress.PlayerCards = _playerCardsData;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _playerCardsData = progress.LevelsProgress.PlayerCards;
            _allCardsSet = progress.LevelsProgress.AllCardsSet;
        }

        private void CreateCardsView(List<CardData> cardsSet)
        {
            if (cardsSet == null) return;

            cardsSet.Sort();

            foreach (CardData cardData in cardsSet)
            {
                GameObject card = CreateCardView(cardData);
                _cards.Add(card);
                card.transform.SetParent(_gridLayout.transform);
            }
        }

        private void CreatePlayerCardsView(List<PlayerCardData> cardsSet)
        {
            if (cardsSet == null) return;

            cardsSet.Sort();

            foreach (PlayerCardData cardData in cardsSet)
            {
                GameObject card = CreateCardView(cardData);
                _cards.Add(card);
                card.transform.SetParent(_gridLayout.transform);

                card.GetComponent<Button>().onClick.AddListener((() => OnCardClick(cardData)));
            }
        }

        private void OnCardClick(PlayerCardData cardData)
        {
            _transactionController.ChooseSelectedCard(cardData);
        }

        private GameObject CreateCardView(CardData cardData)
        {
            GameObject card = _gameFactory.CreateObjectCard();
            SmallCardView smallCardView = card.GetComponent<SmallCardView>();

            smallCardView.Initialize(cardData);

            if (!cardData.IsOpen)
                smallCardView.SetViewToClosed();

            return card;
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