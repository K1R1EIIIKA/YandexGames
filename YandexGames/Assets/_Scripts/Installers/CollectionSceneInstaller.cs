using _Scripts.Controllers;
using _Scripts.Infrastructure.Services.PersistantProgress;
using Zenject;

namespace _Scripts.Installers
{
    public class CollectionSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable), typeof(ICardController), typeof(ISavedProgress))
                .To<CardsController>().AsSingle().NonLazy();
        }
    }
}