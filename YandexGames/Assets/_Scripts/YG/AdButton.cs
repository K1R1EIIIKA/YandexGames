using _Scripts.Controllers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _Scripts.YG
{
    public class AdButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Button _adButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _parentTransform;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _buffText;
        [SerializeField] private Transform _buffStartPosition;

        private AdRewardController _adRewardController;
        private  TransactionController _transactionController;

        [Inject]
        public void Construct(AdRewardController adRewardController, TransactionController transactionController)
        {
            _adRewardController = adRewardController;
            _transactionController = transactionController;
        }
        private void OnEnable()
        {
            _adButton.onClick.AddListener(OnAdButtonClicked);
        }

        private void OnDisable()
        {
            _adButton.onClick.RemoveListener(OnAdButtonClicked);
        }

        private void OnAdButtonClicked()
        {

        }

        public void Show()
        {
            SetRandomPosition();
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0;

            _canvasGroup.DOFade(1, 3f);
        }

        private void SetRandomPosition()
        {
            var rectTransform = GetComponent<RectTransform>();

            var parentRect = _parentTransform.rect;

            var x = Random.Range(-parentRect.width / 2, parentRect.width / 2);
            var y = Random.Range(-parentRect.height / 2, parentRect.height / 2);

            rectTransform.anchoredPosition = new Vector2(x, y);
        }

        public void Hide()
        {
            _canvasGroup.DOFade(0, 3f).OnComplete(() => gameObject.SetActive(false));
        }

        public void InstantHide()
        {
            _canvasGroup.alpha = 0;
            _buffText.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var id = Random.Range(1, AdRewardIds.Count + 1);
            _adRewardController.ShowAd(id);
            _transactionController.AddAdCounter();

            _buffText.gameObject.SetActive(true);

            InstantHide();
            AnimateText(id);
        }

        private void AnimateText(int id)
        {
            _buffText.alpha = 1;
            _buffText.gameObject.SetActive(true);
            _buffText.text = _adRewardController.GetBuffText(id) + " for 10 seconds";
            _buffText.transform.position = _buffStartPosition.position;
            _buffText.transform.DOLocalMoveY(_buffStartPosition.position.y + 50, 4f);
            _buffText.DOFade(0, 3f).SetDelay(1f).OnComplete(() => _buffText.gameObject.SetActive(false));
        }
    }
}