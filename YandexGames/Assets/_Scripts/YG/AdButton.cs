using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _Scripts.YG
{
    public class AdButton : MonoBehaviour
    {
        [SerializeField] private Button _adButton;

        private AdRewardController _adRewardController;

        [Inject]
        public void Construct(AdRewardController adRewardController)
        {
            _adRewardController = adRewardController;
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
            _adRewardController.ShowAd(Random.Range(1, AdRewardIds.Count+1));
        }
    }
}