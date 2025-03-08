using System.Collections.Generic;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        void RegisterProgressWatchers(GameObject registeredWatcher);
        void Register(ISavedProgressReader progressReader);
        void CleanDublicates();
        List<GameObject> CreateObjectCards();
        GameObject CreateObjectCard(GridLayoutGroup gridLayout);
        GameObject CreateObjectBed(GridLayoutGroup gridLayout);
    }
}