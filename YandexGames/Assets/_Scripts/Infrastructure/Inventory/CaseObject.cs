using _Scripts.Controllers;
using _Scripts.Data.Cases;
using _Scripts.ScriptableObjects;
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

        private CaseInfoView _caseInfoView;
        private CaseManager _caseManager;
        private TransactionController _transactionController;

        [Inject]
        public void Construct(CaseInfoView caseInfoView, CaseManager caseManager,
            TransactionController transactionController)
        {
            _caseInfoView = caseInfoView;
            _caseManager = caseManager;
            _transactionController = transactionController;
        }

        public void Initialize(CaseData caseData, int discount = 0)
        {
            _caseData = caseData;
            _caseImage.sprite = _caseData.CaseImage;

            switch (caseData)
            {
                case MoneyCaseData moneyCaseData when moneyCaseData != null:
                    var price = Mathf.RoundToInt(moneyCaseData.Price * (1 - discount / 100f));
                    _casePrice.text = price.ToString();
                    _priceImage.gameObject.SetActive(true);
                    break;

                case AdCaseData adCaseData when adCaseData != null:
                    _casePrice.text = "x" + adCaseData.AdsCount;
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
            _caseManager.TryOpenCase(_caseData);
        }

        private void ShowCaseInfo()
        {
            _caseInfoView.ShowCaseInfo(_caseData);
        }
    }
}