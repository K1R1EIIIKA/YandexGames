using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Infrastructure.AssetManager;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.StaticData;
using _Scripts.Tools;
using _Scripts.View;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticDataService;
        private IPersistantProgressService _progressService;
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressWriters { get; } = new();

        [Inject]
        public void Construct(IAssetProvider assetProvider, IStaticDataService staticDataService,
            IPersistantProgressService progressService)
        {
            _progressService = progressService;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            
            Debug.Log("GameFactory was constructed");
            Debug.Log(_progressService.Progress);
        }

        public void RegisterProgressWatchers(GameObject registeredWatcher)
        {
            foreach (var progressReader in registeredWatcher.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        public List<GameObject> CreateObjectCards()
        {
            List<GameObject> objectCards = new();
            List<CardData> cardsData = _progressService.Progress.LevelsProgress.Cards;

            foreach (CardData cardData in cardsData)
            {
                GameObject card = _assetProvider.Instantiate("Prefabs/UI/Inventory/CardExample");
                CardView cardView = card.GetComponent<CardView>();
                Color cardBackground = cardData.Rare.ToHexColor().ToColor();
                cardView.Initialize(cardBackground, CardSpritesLibrary.LoadSprite(cardData.ImageName), cardData.Name);
                objectCards.Add(card);
            }

            return objectCards;
        }

        public GameObject CreateObjectCard()
        {
            GameObject card = _assetProvider.Instantiate("Prefabs/UI/Inventory/CardExample");

            return card;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}