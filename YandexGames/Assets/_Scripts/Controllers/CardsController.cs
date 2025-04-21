using System.Collections.Generic;
using System.Linq;
using _Scripts.Data;
using _Scripts.Data.Cards;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class CardsController : ISavedProgress, ICardController, IInitializable
    {
        private List<CardData> _allCardsSet = new();
        private List<PlayerCardData> _playerCardsData = new();

        private List<GameObject> _cards = new();

        private readonly IGameFactory _gameFactory;
        private readonly CardBigView _cardBigView;

        private GridLayoutGroup _gridLayout;

        private PlayerCardData _selectedCard;

        [Inject]
        public CardsController(ISaveLoadService saveLoadService, IGameFactory gameFactory,
            CardBigView cardBigView)
        {
            _gameFactory = gameFactory;
            _cardBigView = cardBigView;

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

        public void ShowPlayerBeds()
        {
            ClearCards();
            CreatePlayerCardsView(_playerCardsData);
        }

        public void ShowAllBeds()
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

            foreach (CardData cardData in cardsSet)
            {
                GameObject card = CreateCardView(cardData);
                _cards.Add(card);
                card.transform.SetParent(_gridLayout.transform);

                bool isOpen = _playerCardsData.Exists(x => x.Id == cardData.Id);
                if (!isOpen)
                    card.GetComponent<Button>().onClick.AddListener(() => _cardBigView.OpenCard(cardData));
                else
                    card.GetComponent<Button>().onClick.AddListener(() =>
                        _cardBigView.OpenCard(_playerCardsData.Find(x => x.Id == cardData.Id)));
            }
        }

        private void CreatePlayerCardsView(List<PlayerCardData> cardsSet)
        {
            if (cardsSet == null) return;

            foreach (PlayerCardData cardData in cardsSet)
            {
                GameObject card = CreateCardView(cardData);
                _cards.Add(card);

                card.GetComponent<Button>().onClick.AddListener(() => _cardBigView.OpenCard(cardData));
            }
        }

        private GameObject CreateCardView(CardData cardData)
        {
            GameObject card = _gameFactory.CreateObjectCard(_gridLayout);
            SmallCardView smallCardView = card.GetComponent<SmallCardView>();

            smallCardView.Initialize(cardData);

            if (!cardData.IsOpen)
                smallCardView.SetViewToClosed();

            return smallCardView.gameObject;
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

        public void SortInventoryCards(InventoryCardsSortType sortType)
        {
            switch (sortType)
            {
                case InventoryCardsSortType.ByCountDesc:
                    _playerCardsData.Sort((a, b) =>
                    {
                        if (a.Count == b.Count)
                        {
                            return a.Rarity.CompareTo(b.Rarity);
                        }

                        return b.Count.CompareTo(a.Count);
                    });
                    break;
                case InventoryCardsSortType.ByCountAsc:
                    _playerCardsData.Sort((a, b) =>
                    {
                        if (a.Count == b.Count)
                        {
                            return a.Rarity.CompareTo(b.Rarity);
                        }

                        return a.Count.CompareTo(b.Count);
                    });
                    break;
                case InventoryCardsSortType.NyRareDesc:
                    _playerCardsData.Sort((a, b) =>
                    {
                        if (a.Rarity == b.Rarity)
                        {
                            return b.Count.CompareTo(a.Count);
                        }

                        return b.Rarity.CompareTo(a.Rarity);
                    });
                    break;
                case InventoryCardsSortType.ByRareAsc:
                    _playerCardsData.Sort((a, b) =>
                    {
                        if (a.Rarity == b.Rarity)
                        {
                            return a.Count.CompareTo(b.Count);
                        }

                        return a.Rarity.CompareTo(b.Rarity);
                    });
                    break;
            }
        }

        public void SortCollectionCards(CollectionSortType sortType)
        {
            _allCardsSet = sortType switch
            {
                CollectionSortType.ByHasDesc => _allCardsSet.OrderByDescending(c => c.IsOpen).ThenBy(c => c.Rarity)
                    .ToList(),
                CollectionSortType.ByHasAsc => _allCardsSet.OrderBy(c => c.IsOpen).ThenBy(c => c.Rarity).ToList(),
                CollectionSortType.ByRareAsc => _allCardsSet.OrderBy(c => c.Rarity).ThenByDescending(c => c.IsOpen)
                    .ToList(),
                CollectionSortType.ByRareDesc => _allCardsSet.OrderByDescending(c => c.Rarity)
                    .ThenByDescending(c => c.IsOpen).ToList(),
                _ => _allCardsSet
            };
        }
    }


    public enum InventoryCardsSortType
    {
        ByRareAsc,
        NyRareDesc,
        ByCountDesc,
        ByCountAsc,
    }

    public enum CollectionSortType
    {
        ByHasDesc,
        ByHasAsc,
        ByRareAsc,
        ByRareDesc,
    }

    public enum SortType
    {
        Rarity,
        Count,
        Availability
    }
}