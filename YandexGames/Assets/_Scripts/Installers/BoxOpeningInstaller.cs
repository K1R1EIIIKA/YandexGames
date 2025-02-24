using _Scripts.Controllers;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class BoxOpeningInstaller : MonoInstaller
    {
        [SerializeField] private BoxOpeningController _boxOpeningController;

        public override void InstallBindings()
        {
            if (BoxOpeningController.Instance != null)
            {
                Container.Bind<BoxOpeningController>().FromInstance(BoxOpeningController.Instance).AsSingle();
            }
        }
    }
}