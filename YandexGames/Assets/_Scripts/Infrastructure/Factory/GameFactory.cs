using System.Collections.Generic;
using System.Linq;
using _Scripts.Infrastructure.AssetManager;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticDataService;
        private IPersistantProgressService _progressService;
        public List<ISavedProgressReader> ProgressReaders { get; private set; } = new();
        public List<ISavedProgress> ProgressWriters { get; private set; } = new();

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
            // List<GameObject> objectCards = new();
            // List<CardData> cardsData = _progressService.Progress.LevelsProgress.PlayerCards;
            //
            // foreach (CardData cardData in cardsData)
            // {
            //     GameObject card = _assetProvider.Instantiate("Prefabs/UI/Collection/CardExample");
            //     CardView cardView = card.GetComponent<CardView>();
            //     Color cardBackground = cardData.Rare.ToHexColor().ToColor();
            //     cardView.Initialize(cardBackground, CardSpritesLibrary.LoadSprite(cardData.ImageName), cardData.Name);
            //     objectCards.Add(card);
            // }
            //
            // return objectCards;
            return new List<GameObject>();
        }

        public GameObject CreateObjectCard()
        {
            GameObject card = _assetProvider.Instantiate("Prefabs/UI/Collection/SmallCard");

            return card;
        }

        public GameObject CreateObjectBed()
        {
            GameObject bed = _assetProvider.Instantiate("Prefabs/UI/Collection/BedInventory");

            return bed;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }

        public void CleanDublicates()
        {
            // Убираем null элементы и дубликаты в ProgressReaders
            ProgressReaders = ProgressReaders
                .Where(reader => reader != null) // Убираем null элементы
                .GroupBy(reader => reader.GetType()) // Группируем по типу, чтобы избавиться от дубликатов
                .Select(group => group.First()) // Берем первый элемент из каждой группы
                .ToList();

            // Убираем null элементы и дубликаты в ProgressWriters
            ProgressWriters = ProgressWriters
                .Where(writer => writer != null) // Убираем null элементы
                .GroupBy(writer => writer.GetType()) // Группируем по типу
                .Select(group => group.First()) // Берем первый элемент из каждой группы
                .ToList();

            Debug.Log("Removed nulls and duplicates from ProgressReaders and ProgressWriters");
        }
    }
}