using _Scripts.Controllers;
using _Scripts.Data.Cases;
using _Scripts.Enums;
using _Scripts.Infrastructure.Core.States;
using _Scripts.Tools;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Inventory
{
    public class CaseManager : MonoBehaviour
    {
        [SerializeField] private CaseRepository _caseRepository;
        [SerializeField] private CaseObject _caseObjectPrefab;

        private DiContainer _container;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(DiContainer container, GameStateMachine gameStateMachine)
        {
            _container = container;
            _gameStateMachine = gameStateMachine;
        }

        public void InitializeCases(RectTransform container, CaseLocationType caseType)
        {
            var cases = caseType switch
            {
                CaseLocationType.MainScreen => _caseRepository.MainScreenCases,
                CaseLocationType.Shop => _caseRepository.ShopCases,
                _ => throw new System.ArgumentOutOfRangeException()
            };

            container.DestroyAllChildren();

            foreach (var caseData in cases)
            {
                var caseObject = _container.InstantiatePrefabForComponent<CaseObject>(_caseObjectPrefab, container);
                caseObject.Initialize(caseData);
            }
        }

        public void InitializeCases(RectTransform container, CaseData[] cases)
        {
            container.DestroyAllChildren();

            foreach (var caseData in cases)
            {
                var caseObject = _container.InstantiatePrefabForComponent<CaseObject>(_caseObjectPrefab, container);
                caseObject.Initialize(caseData);
            }
        }

        public void OpenCaseScene(CaseData caseData)
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.BoxOpening, () =>
            {
                BoxOpeningController.Instance.Initialize(caseData);
            });
        }
    }
}