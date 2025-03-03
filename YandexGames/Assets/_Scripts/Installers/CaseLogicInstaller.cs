using _Scripts.Infrastructure.Inventory;
using _Scripts.View;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class CaseLogicInstaller : MonoInstaller
    {
        [SerializeField] CaseInfoView _caseInfoView;

        public override void InstallBindings()
        {
            Container.Bind<CaseInfoView>().FromComponentInNewPrefab(_caseInfoView).AsSingle().NonLazy();
        }
    }
}