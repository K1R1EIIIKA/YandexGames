using System;
using _Scripts.BuffLogic;
using _Scripts.Controllers;
using _Scripts.Data.Cases;
using _Scripts.Enums;
using _Scripts.Infrastructure.Core.States;
using _Scripts.ScriptableObjects;
using _Scripts.Tools;
using _Scripts.YG;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Inventory
{
    public class CaseManager : MonoBehaviour
    {
        [SerializeField] private CaseRepository _caseRepository;
        [SerializeField] private CaseObject _caseObjectPrefab;
        [SerializeField] private CaseObject _smallCaseObjectPrefab;

        private DiContainer _container;
        private GameStateMachine _gameStateMachine;
        private TransactionController _transactionController;
        private BuffController _buffController;
        private AdRewardController _adRewardController;

        private  CaseData _currentCaseData;

        [Inject]
        public void Construct(DiContainer container, GameStateMachine gameStateMachine,
            TransactionController transactionController, BuffController buffController,
            AdRewardController adRewardController)
        {
            _container = container;
            _gameStateMachine = gameStateMachine;
            _transactionController = transactionController;
            _buffController = buffController;
            _adRewardController = adRewardController;

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

        public void InitializeAdCases(RectTransform container)
        {
            container.DestroyAllChildren();

            var cases = _caseRepository.AdCases;
            foreach (var caseData in cases)
            {
                var caseObject =
                    _container.InstantiatePrefabForComponent<CaseObject>(_smallCaseObjectPrefab, container);
                caseObject.Initialize(caseData);
            }
        }

        public void TryOpenCase(CaseData caseData)
        {
            _currentCaseData = caseData;

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

            else if (caseData is AdCaseData adCaseData)
            {
                _adRewardController.AdCaseAdId += OpenAdCaseAction;
                _adRewardController.ShowAd(AdRewardIds.AdCaseId);
            }
        }

        private void OpenAdCaseAction()
        {
            OpenAdCase(_currentCaseData as AdCaseData);
        }

        private void OpenAdCase(AdCaseData adCaseData)
        {
            _adRewardController.AdCaseAdId -= OpenAdCaseAction;
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.BoxOpening,
                () => { BoxOpeningController.Instance.Initialize(adCaseData); });
        }
    }
}