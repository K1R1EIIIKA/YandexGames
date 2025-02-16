using _Scripts.Controllers;
using _Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class InventorySceneInstaller : MonoInstaller
    {
        [SerializeField] private InventoryTabsController _inventoryTabsController;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable), typeof(ICardController), typeof(ISavedProgress))
                .To<CardsController>().AsSingle().NonLazy();

            // Убираем дублирующуюся строку
            // Container.Bind<CardsController>().AsSingle().NonLazy();

            if (InventoryTabsController.Instance != null)
            {
                Container.Bind<InventoryTabsController>().FromInstance(InventoryTabsController.Instance).AsSingle();
            }

            // Container.Bind<InventoryTabsController>().FromInstance(_inventoryTabsController).AsSingle().NonLazy();

            Container.Bind<InventoryController>().AsSingle().NonLazy();
        }
    }
}