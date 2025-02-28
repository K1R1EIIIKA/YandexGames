using System.Collections.Generic;
using System.Linq;
using _Scripts.Data;
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

        private List<PlayerCardData> _playerCards = new List<PlayerCardData>();
        private List<CardData> _allCards = new List<CardData>();
        public List<PlayerCardData> PlayerCards => _playerCards;
        public List<CardData> AllCards => _allCards;

        private PlayerCardData _selectedCard;
        public PlayerCardData SelectedCard => _selectedCard;

        private GameFactory _gameFactory;

        [Inject]
        public void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;

            _gameFactory.Register(this);
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
                Debug.Log(playerCard.Name + " " + playerCard.Count);
            }
        }

        private void OpenCard(CardData card)
        {
            card.IsOpen = true;
        }

        public void ChooseSelectedCard(PlayerCardData card)
        {
            _selectedCard = card;
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


        public void LoadProgress(PlayerProgress progress)
        {
            _money = progress.LevelsProgress.Money;
            _playerCards = progress.LevelsProgress.PlayerCards;
            _allCards = progress.LevelsProgress.AllCardsSet;
            _selectedCard = progress.LevelsProgress.SelectedCard;

            CheckForNewCards(Resources.LoadAll<CardObject>("Cards").ToList());
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            Debug.Log(_money + " " + progress.LevelsProgress.Money);
            progress.LevelsProgress.Money = _money;
            progress.LevelsProgress.PlayerCards = _playerCards;
            progress.LevelsProgress.AllCardsSet = _allCards;
            progress.LevelsProgress.SelectedCard = _selectedCard;
        }
    }
}