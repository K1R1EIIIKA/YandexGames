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
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private CardView cardPrefab;

        private List<GameObject> cards;
        private IGameFactory _gameFactory;

        [Header("Test")] [SerializeField] List<Sprite> cardSprites;
        [Inject] private IPersistantProgressService _progressService;
        private List<CardData> cardDatas = new List<CardData>();
        [Inject] private ISaveLoadService _saveLoadService;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Awake()
        {
            //получить данные из прогресса - геймфактори
            //создать все объекты в инвентарь - геймфактори
            //проинициализировать всю хуйню - геймфактори

            _gameFactory.Register(this);
            cards = _gameFactory.CreateObjectCards();
            Debug.Log(cards.Count);

            if (cards.Count == 0)
            {
                foreach (Sprite cardSprite in cardSprites)
                {
                    GameObject cardObject = _gameFactory.CreateObjectCard();
                    CardView cardView = cardObject.GetComponent<CardView>();
                    cardView.Initialize(Color.cyan, cardSprite, cardSprite.name);
                    CardData cardData = new CardData();
                    cardData.Rare = Rare.SuperMegaRare;
                    cardData.Name = cardSprite.name;
                    cards.Add(cardObject);
                    cardDatas.Add(cardData);
                }
                _saveLoadService.SaveProgress();
            }

            foreach (GameObject card in cards)
            {
                card.transform.SetParent(gridLayout.transform);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.LevelsProgress.Cards = cardDatas;
        }
    }
}