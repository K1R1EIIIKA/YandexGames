using System.Linq;
using _Scripts.Controllers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _Scripts.YG
{
    public class AdButton : MonoBehaviour
    {
        [SerializeField] private Button _adButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _parentTransform;
        [SerializeField] private float _adChance = 0.2f;

        [Header("Text")] [SerializeField] private TextMeshProUGUI _buffText;
        [SerializeField] private Transform _buffStartPosition;
        [SerializeField] private Image _adImage;
        [SerializeField] private AdWindow _adWindow;

        private AdRewardController _adRewardController;
        private TransactionController _transactionController;
        private bool _isAd;

        [Inject]
        public void Construct(AdRewardController adRewardController, TransactionController transactionController)
        {
            _adRewardController = adRewardController;
            _transactionController = transactionController;
        }

        private void OnEnable()
        {
            _adButton.onClick.AddListener(OnPointerClick);
        }

        private void OnDisable()
        {
            _adButton.onClick.RemoveListener(OnPointerClick);
        }

        public void Show()
        {
            _isAd = Random.value < _adChance;
            _adImage.gameObject.SetActive(_isAd);

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

        public void OnPointerClick()
        {
            Debug.Log("BUFFFFFFFFFF");

            if (_isAd)
            {
                var validIds = Enumerable.Range(1, AdRewardIds.Count)
                    .Where(x => x != 3 && x != 6 && x != 11)
                    .ToList();

                var id = validIds[Random.Range(0, validIds.Count)];
                _adWindow.Show(id, () => OnAdWatched(id));
                InstantHide();
            }
            else
            {
                var validIds = Enumerable.Range(1, AdRewardIds.Count)
                    .Where(x => x != 7 && x != 8 && x != 9)
                    .ToList();

                var id = validIds[Random.Range(0, validIds.Count)];
                _adRewardController.OnRewardVideo(id, false);
                _buffText.gameObject.SetActive(true);

                InstantHide();
                AnimateText(id, 10);
            }
        }

        private void OnAdWatched(int id)
        {
            _transactionController.AddAdCounter();

            _buffText.gameObject.SetActive(true);

            AnimateText(id, 60);
        }

        private void AnimateText(int id, int duration)
        {
            _buffText.alpha = 1;
            _buffText.gameObject.SetActive(true);
            _buffText.text = _adRewardController.GetBuffText(id) + $" for {duration} seconds";
            _buffText.transform.position = _buffStartPosition.position;
            _buffText.transform.DOLocalMoveY(_buffStartPosition.position.y + 50, 4f);
            _buffText.DOFade(0, 3f).SetDelay(1f).OnComplete(() => _buffText.gameObject.SetActive(false));
        }
    }
}