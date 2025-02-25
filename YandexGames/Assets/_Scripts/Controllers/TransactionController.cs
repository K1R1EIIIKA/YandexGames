using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Data.Cards;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace _Scripts.Controllers
{
    public class TransactionController : ISavedProgress
    {
        private int _money;
        public int Money => _money;

        private List<CardData> _playerCards;
        public List<CardData> PlayerCards => _playerCards;

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
            _playerCards.AddRange(cards);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _money = progress.LevelsProgress.Money;
            _playerCards = progress.LevelsProgress.PlayerCards;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            Debug.Log(_money + " " + progress.LevelsProgress.Money);
            progress.LevelsProgress.Money = _money;
            progress.LevelsProgress.PlayerCards = _playerCards;
        }
    }
}