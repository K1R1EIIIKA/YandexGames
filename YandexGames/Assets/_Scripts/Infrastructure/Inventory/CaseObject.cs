using System.Globalization;
using _Scripts.Controllers;
using _Scripts.ScriptableObjects;
using _Scripts.Tools;
using _Scripts.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Infrastructure.Inventory
{
    public class CaseObject : MonoBehaviour
    {
        [Header("Case Data")] [SerializeField] private CaseData _caseData;

        [Header("UI Elements")] [SerializeField]
        private Image _caseImage;

        [SerializeField] private TextMeshProUGUI _casePrice;
        [SerializeField] private Button _openCaseButton;
        [SerializeField] private Button _caseInfoButton;
        [SerializeField] private Image _priceImage;
        [SerializeField] private Image _adCountImage;
        [SerializeField] private Image _totalCasesImage;

        private CaseInfoView _caseInfoView;
        private CaseManager _caseManager;
        private TransactionController _transactionController;
        private CaseController _caseController;

        [Inject]
        public void Construct(CaseInfoView caseInfoView, CaseManager caseManager,
            TransactionController transactionController, CaseController caseController)
        {
            _caseInfoView = caseInfoView;
            _caseManager = caseManager;
            _transactionController = transactionController;
            _caseController = caseController;
        }

        public void Initialize(CaseData caseData, int discount = 0)
        {
            _caseData = caseData;
            _caseImage.sprite = _caseData.CaseImage;

            switch (caseData)
            {
                case MoneyCaseData moneyCaseData when moneyCaseData != null:
                    if (!moneyCaseData.IsTotalCases)
                    {
                        var casePrice = _caseController.GetTotalMoneyPrice(moneyCaseData);
                        var moneyPrice = BigNumberFormatter.FormatBigNumber(casePrice * (1 - discount / 100f));
                        _casePrice.text = moneyPrice.ToString(CultureInfo.InvariantCulture);
                        _priceImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        _casePrice.text = _transactionController.CaseCounter + "/" + moneyCaseData.Price;
                        _totalCasesImage.gameObject.SetActive(true);
                        _priceImage.gameObject.SetActive(false);
                    }

                    break;

                case LimitedCaseData limitedCaseData when limitedCaseData != null:
                    var limitedPrice = BigNumberFormatter.FormatBigNumber(limitedCaseData.Price * (1 - discount / 100f));
                    _casePrice.text = limitedPrice.ToString();
                    _priceImage.gameObject.SetActive(true);

                    break;

                case AdCaseData adCaseData when adCaseData != null:
                    if (!adCaseData.IsTotalAdsCount)
                    {
                        _casePrice.text = "x" + adCaseData.AdsCount;
                    }
                    else
                    {
                        _casePrice.text = _transactionController.AdCounter + "/" + adCaseData.AdsCount;
                    }

                    _adCountImage.gameObject.SetActive(true);
                    _priceImage.gameObject.SetActive(false);
                    break;
            }
        }

        private void OnEnable()
        {
            _openCaseButton.onClick.AddListener(OpenCase);
            _caseInfoButton.onClick.AddListener(ShowCaseInfo);
        }

        private void OnDisable()
        {
            _openCaseButton.onClick.RemoveListener(OpenCase);
            _caseInfoButton.onClick.RemoveListener(ShowCaseInfo);
        }

        private void OpenCase()
        {
            _caseManager.TryOpenCase(_caseData, transform);
        }

        private void ShowCaseInfo()
        {
            _caseInfoView.ShowCaseInfo(_caseData);
        }
    }
}