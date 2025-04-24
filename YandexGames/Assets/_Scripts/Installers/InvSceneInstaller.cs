using System.ComponentModel;
using _Scripts.View;
using UnityEngine;
using Zenject;

public class InvSceneInstaller : MonoInstaller
{
    [SerializeField] private SmallCardView _smallCardPrefab; // Префаб, а не объект сцены

    public override void InstallBindings()
    {
        Container.Bind<SmallCardViewFactory>()
            .AsSingle()
            .WithArguments(_smallCardPrefab);
    }

}