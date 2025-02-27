using System;
using _Scripts.Controllers;
using _Scripts.Infrastructure.AssetManager;
using _Scripts.Infrastructure.Core;
using _Scripts.Infrastructure.Core.SceneTransitions;
using _Scripts.Infrastructure.Core.States;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public override void InstallBindings()
        {
            BindStateMachine();
            BindFactories();
            BindProgressServices();
            Debug.Log($"[{nameof(ProjectInstaller)}] InstallBindings");
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle().NonLazy();
        }

        private void BindStateMachine()
        {
            Container.Bind<SceneLoader>().To<SceneLoader>().AsSingle().NonLazy();
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle().NonLazy();

            Container.Bind<StateFactory>().To<StateFactory>().AsTransient().NonLazy();

            Container.Bind<PayloadedStateFactory<string>>().FromNew().AsTransient().NonLazy();

            Container.Bind(typeof(IGameStateMachine), typeof(IDisposable),
                typeof(GameStateMachine)).To<GameStateMachine>().AsSingle().NonLazy();
        }

        private void BindProgressServices()
        {
            Container.Bind<IPersistantProgressService>().To<ProgressService>().AsSingle().NonLazy();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle().NonLazy();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle().NonLazy();
            Container.Bind<TransactionController>().AsSingle().NonLazy();
        }
    }
}