using System.Collections.Generic;
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
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressWriters { get; } = new();

        [Inject]
        public void Construct(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public void RegisterProgressWatchers(GameObject registeredWatcher)
        {
            foreach (var progressReader in registeredWatcher.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
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