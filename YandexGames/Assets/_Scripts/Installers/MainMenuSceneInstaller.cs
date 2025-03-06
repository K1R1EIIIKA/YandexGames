using _Scripts.View;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private CardBigView _cardBigView;

        public override void InstallBindings()
        {
            Container.Bind<CardBigView>().FromInstance(_cardBigView).AsSingle().NonLazy();
        }
    }
}