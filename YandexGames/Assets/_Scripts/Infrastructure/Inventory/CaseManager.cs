using System;
using _Scripts.Controllers;
using _Scripts.Data.Cases;
using _Scripts.Enums;
using _Scripts.Infrastructure.Core.States;
using _Scripts.ScriptableObjects;
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
        private TransactionController _transactionController;

        [Inject]
        public void Construct(DiContainer container, GameStateMachine gameStateMachine,
            TransactionController transactionController)
        {
            _container = container;
            _gameStateMachine = gameStateMachine;
            _transactionController = transactionController;
        }

        public void InitializeCases(RectTransform container, CaseLocationType caseType)
        {
            var cases = caseType switch
            {
                CaseLocationType.MainScreen => _caseRepository.MainScreenCases,
                CaseLocationType.Shop => _caseRepository.ShopCases,
                _ => throw new ArgumentOutOfRangeException()
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

        public void TryOpenCase(CaseData caseData)
        {
            if (caseData is MoneyCaseData moneyCaseData)
            {
                if (_transactionController.SpendMoney(moneyCaseData.Price))
                {
                    _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.BoxOpening,
                        () => { BoxOpeningController.Instance.Initialize(caseData); });
                }

                else
                {
                    Debug.Log("Not enough money");
                }
            }
        }
    }
}