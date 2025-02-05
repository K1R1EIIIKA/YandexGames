using System;
using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
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

        private List<GameObject> _cards;
        private IGameFactory _gameFactory;

        private List<CardData> _cardDatas = new List<CardData>();
        [Inject] private ISaveLoadService _saveLoadService;

        [Header("Test")] [SerializeField] List<Sprite> cardSprites;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Awake()
        {
            _gameFactory.Register(this);
            _cards = _gameFactory.CreateObjectCards();
            Debug.Log(_cards.Count);

            if (_cards.Count == 0)
            {
                foreach (Sprite cardSprite in cardSprites)
                {
                    GameObject cardObject = _gameFactory.CreateObjectCard();
                    CardView cardView = cardObject.GetComponent<CardView>();
                    cardView.Initialize(Color.cyan, cardSprite, cardSprite.name);

                    CardData cardData = new CardData(_cardDatas.Count.ToString(), cardSprite.name, cardSprite.name, 15,
                        Rare.Advertisement, true);
                    _cards.Add(cardObject);
                    _cardDatas.Add(cardData);
                }

               

                _saveLoadService.SaveProgress();
            }

            foreach (GameObject card in _cards)
            {
                card.transform.SetParent(_gridLayout.transform);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.LevelsProgress.Cards = _cardDatas;
        }
    }
}