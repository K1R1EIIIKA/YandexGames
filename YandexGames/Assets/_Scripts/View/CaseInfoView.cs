using System.Collections.Generic;
using System.Linq;
using _Scripts.Controllers;
using _Scripts.Data.Cards;
using _Scripts.ScriptableObjects;
using _Scripts.Tools;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.View
{
    public class CaseInfoView : MonoBehaviour
    {
        [SerializeField] private Image _caseImage;
        [SerializeField] private TextMeshProUGUI _caseName;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private RectTransform _caseContent;
        [SerializeField] private Button _closeButton;

        [SerializeField] CardView _cardViewPrefab;

        [Inject] private TransactionController _transactionController;

        private CaseData _caseData;

        private void Awake()
        {
            HideCaseInfo();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(HideCaseInfo);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(HideCaseInfo);
        }

        public void ShowCaseInfo(CaseData caseData)
        {
            _caseContent.DestroyAllChildren();

            _caseData = caseData;
            _caseImage.sprite = _caseData.CaseImage;
            _caseName.text = _caseData.GetName();
            _coinsText.text = $"{BigNumberFormatter.FormatBigNumber(_caseData.CoinsRange.x)}-{BigNumberFormatter.FormatBigNumber(_caseData.CoinsRange.y)}";

            var sortedCards = _caseData.CardPool
                .OrderBy(card => card.Rarity)
                .ThenBy(card => card.Cost)
                .ToList();

            foreach (var cardData in sortedCards)
            {
                var cardView = Instantiate(_cardViewPrefab, _caseContent);
                cardView.Initialize(cardData.ToCardData());

                if (_transactionController.IsCardClosed(cardData))
                {
                    cardView.SetViewToClosed();
                }
            }

            gameObject.SetActive(true);
        }

        private void HideCaseInfo()
        {
            gameObject.SetActive(false);
        }
    }
}