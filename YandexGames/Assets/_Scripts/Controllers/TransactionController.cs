using System.Collections.Generic;
using System.Linq;
using _Scripts.BuffLogic;
using _Scripts.Data;
using _Scripts.Data.Beds;
using _Scripts.Data.Cards;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;
using Zenject;

namespace _Scripts.Controllers
{
    public class TransactionController : ISavedProgress
    {
        private int _money;
        public int Money => _money;

        private List<PlayerCardData> _playerCards = new();
        private List<CardData> _allCards = new();
        public List<PlayerCardData> PlayerCards => _playerCards;
        public List<CardData> AllCards => _allCards;

        private List<BedData> _playerBeds = new();
        private List<BedData> _allBeds = new();
        public List<BedData> PlayerBeds => _playerBeds;
        public List<BedData> AllBeds => _allBeds;

        private PlayerCardData _selectedCard;
        public PlayerCardData SelectedCard => _selectedCard;

        private BedData _selectedBed;
        public BedData SelectedBed => _selectedBed;

        private GameFactory _gameFactory;
        private BuffController _buffController;

        [Inject]
        public void Construct(GameFactory gameFactory, BuffController buffController)
        {
            _gameFactory = gameFactory;
            _buffController = buffController;

            _gameFactory.Register(this);

            _buffController.Initialize(new BuffStats());
        }

        public void AddMoney(int amount)
        {
            _money += amount;
        }

        public bool SpendMoney(int amount)
        {
            if (_money < amount)
            {
                return false;
            }

            _money -= amount;
            return true;
        }

        public void AddPlayerCards(List<CardData> cards)
        {
            if (_playerCards == null)
            {
                _playerCards = new List<PlayerCardData>();
            }

            foreach (var card in cards)
            {
                bool isCardExist = false;

                foreach (var playerCard in _playerCards)
                {
                    if (playerCard.Id == card.Id)
                    {
                        playerCard.Count++;
                        isCardExist = true;

                        if (!card.IsOpen)
                        {
                            OpenCard(card);
                        }
                        break;
                    }
                }

                if (!isCardExist)
                {
                    _playerCards.Add(new PlayerCardData(card));
                    OpenCard(card);
                }
            }

            Debug.Log("Player cards: ");
            foreach (var playerCard in _playerCards)
            {
                if (_selectedCard != null && playerCard.Id == _selectedCard.Id)
                {
                    _selectedCard = playerCard;
                }
                Debug.Log(playerCard.Name + " " + playerCard.Count);
            }
        }

        private void OpenCard(CardData card)
        {
            card.IsOpen = true;
        }

        public int GetPlayerCardsCount()
        {
            var count = 0;

            foreach (var playerCard in _playerCards)
            {
                count += playerCard.Count;
            }

            return count;
        }

        public void ChooseSelectedCard(PlayerCardData card)
        {
            _selectedCard = card;
        }

        public void ChooseSelectedBed(BedData bed)
        {
            _selectedBed = bed;
        }

        public void CheckForNewCards(List<CardObject> cardPool)
        {
            if (_allCards == null)
            {
                _allCards = new List<CardData>();
            }

            foreach (var card in cardPool)
            {
                bool isNewCard = !_allCards.Exists(c => c.Id == card.Id);

                if (isNewCard)
                {
                    _allCards.Add(new CardData(card));
                    Debug.Log($"New card added: {card.Name}");
                }
                else
                {
                    foreach (var playerCard in _playerCards)
                    {
                        if (playerCard.Id == card.Id)
                        {
                            _allCards.Find(c => c.Id == card.Id).IsOpen = true;
                        }
                    }
                }
            }
        }

        private void CheckForNewBeds(List<BedObject> bedPool)
        {
            if (_allBeds == null)
            {
                _allBeds = new List<BedData>();
            }

            foreach (var bed in bedPool)
            {
                bool isNewBed = !_allBeds.Exists(b => b.Id == bed.Id);

                if (isNewBed)
                {
                    _allBeds.Add(new BedData(bed));
                    Debug.Log($"New bed added: {bed.Name}");
                }
                else
                {
                    foreach (var playerBed in _playerBeds)
                    {
                        if (playerBed.Id == bed.Id)
                        {
                            _allBeds.Find(b => b.Id == bed.Id).IsOpen = true;
                        }
                    }
                }
            }
        }


        public void LoadProgress(PlayerProgress progress)
        {
            _money = progress.LevelsProgress.Money;
            _playerCards = progress.LevelsProgress.PlayerCards;
            _allCards = progress.LevelsProgress.AllCardsSet;
            _selectedCard = progress.LevelsProgress.SelectedCard;

            _playerBeds = progress.LevelsProgress.PlayerBeds;
            _allBeds = progress.LevelsProgress.AllBedsSet;
            _selectedBed = progress.LevelsProgress.SelectedBed;

            CheckForNewCards(Resources.LoadAll<CardObject>("Cards").ToList());
            CheckForNewBeds(Resources.LoadAll<BedObject>("Beds").ToList());
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.LevelsProgress.Money = _money;
            progress.LevelsProgress.PlayerCards = _playerCards;
            progress.LevelsProgress.AllCardsSet = _allCards;
            progress.LevelsProgress.SelectedCard = _selectedCard;

            progress.LevelsProgress.PlayerBeds = _playerBeds;
            progress.LevelsProgress.AllBedsSet = _allBeds;
            progress.LevelsProgress.SelectedBed = _selectedBed;
        }
    }
}