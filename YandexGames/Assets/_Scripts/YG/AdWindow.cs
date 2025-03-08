using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.YG
{
    public class AdWindow : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _watchButton;

        [Inject] private AdRewardController _adRewardController;

        private int _id;
        private Action _onAdWatched;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _watchButton.onClick.AddListener(OnWatchButtonClicked);
        }

        private void OnWatchButtonClicked()
        {
            _adRewardController.ShowAd(_id, _onAdWatched);
            Hide();
        }

        private void OnCloseButtonClicked()
        {
            Hide();
        }

        public void Show(int id, Action onAdWatched)
        {
            _id = id;
            _onAdWatched = onAdWatched;
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}