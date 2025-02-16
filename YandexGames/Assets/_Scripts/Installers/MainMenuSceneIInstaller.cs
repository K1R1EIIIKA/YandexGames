using _Scripts.Controllers;
using Zenject;

namespace _Scripts.Installers
{
    public class MainMenuSceneIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InventoryTabsController>().FromMethod(_ => InventoryTabsController.Instance).AsSingle();
        }

    }
}