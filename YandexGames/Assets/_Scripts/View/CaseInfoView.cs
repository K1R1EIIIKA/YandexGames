using _Scripts.Data.Cases;
using _Scripts.ScriptableObjects;
using _Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.View
{
    public class CaseInfoView : MonoBehaviour
    {
        [SerializeField] private Image _caseImage;
        [SerializeField] private TextMeshProUGUI _caseName;
        [SerializeField] private RectTransform _caseContent;
        [SerializeField] private Button _closeButton;

        [SerializeField] CardView _cardViewPrefab;

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
            _caseName.text = _caseData.Name;

            foreach (var cardData in _caseData.CardPool)
            {
                var cardView = Instantiate(_cardViewPrefab, _caseContent);
                cardView.Initialize(cardData.ToCardData());
            }

            gameObject.SetActive(true);
        }

        private void HideCaseInfo()
        {
            gameObject.SetActive(false);
        }
    }
}