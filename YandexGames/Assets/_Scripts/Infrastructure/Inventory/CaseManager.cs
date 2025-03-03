using System;
using _Scripts.BuffLogic;
using _Scripts.Controllers;
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
        private BuffController _buffController;

        [Inject]
        public void Construct(DiContainer container, GameStateMachine gameStateMachine,
            TransactionController transactionController, BuffController buffController)
        {
            _container = container;
            _gameStateMachine = gameStateMachine;
            _transactionController = transactionController;
            _buffController = buffController;

            Debug.Log("CaseManager initialized");
        }

        private int GetDiscount()
        {
            return _buffController.CurrentStats.DiscountBonus;
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
            var discount = GetDiscount();

            foreach (var caseData in cases)
            {
                var caseObject = _container.InstantiatePrefabForComponent<CaseObject>(_caseObjectPrefab, container);
                caseObject.Initialize(caseData, discount);
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
                var discount = GetDiscount();
                var price = Mathf.RoundToInt(moneyCaseData.Price * (1 - discount / 100f));
                if (_transactionController.SpendMoney(price))
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