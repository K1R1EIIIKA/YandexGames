using _Scripts.Infrastructure.Inventory;
using _Scripts.View;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class CaseLogicInstaller : MonoInstaller
    {
        [SerializeField] CaseManager _caseManager;
        [SerializeField] CaseInfoView _caseInfoView;

        public override void InstallBindings()
        {
            Container.Bind<CaseManager>().FromInstance(_caseManager).AsSingle().NonLazy();
            Container.Inject(_caseManager); // Теперь вызовется Construct

            Container.Bind<CaseInfoView>().FromComponentInNewPrefab(_caseInfoView).AsSingle().NonLazy();
        }
    }
}