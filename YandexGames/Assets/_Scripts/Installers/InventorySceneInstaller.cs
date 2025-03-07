using _Scripts.Controllers;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.View;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class InventorySceneInstaller : MonoInstaller
    {
        [SerializeField] private InventoryTabsController _inventoryTabsController;
        [SerializeField] private CardBigView _cardBigView;
        [SerializeField] private BedBigView _bedBigView;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable), typeof(ICardController), typeof(ISavedProgress))
                .To<CardsController>().AsSingle().NonLazy();

            Container.Bind<BedsController>().AsSingle().NonLazy();

            // Убираем дублирующуюся строку
            // Container.Bind<CardsController>().AsSingle().NonLazy();

            if (InventoryTabsController.Instance != null)
            {
                Container.Bind<InventoryTabsController>().FromInstance(InventoryTabsController.Instance).AsSingle();
            }

            // Container.Bind<InventoryTabsController>().FromInstance(_inventoryTabsController).AsSingle().NonLazy();

            Container.Bind<CardBigView>().FromComponentInNewPrefab(_cardBigView).AsSingle().NonLazy();
            Container.Bind<BedBigView>().FromComponentInNewPrefab(_bedBigView).AsSingle().NonLazy();
            Container.Bind<InventoryController>().AsSingle().NonLazy();
        }
    }
}