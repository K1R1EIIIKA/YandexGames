using System;
using UnityEngine;
using YG;

namespace _Scripts.YG
{
    public class AdRewardController
    {
        public event Action OneAndHalfMoneyAdId;
        public event Action DoubleMoneyAdId;
        public event Action TripleMoneyAdId;
        public event Action Discount10AdId;
        public event Action Discount20AdId;
        public event Action Discount50AdId;
        public event Action BigCharacterAdId;
        public event Action SmallCharacterAdId;
        public event Action CrazyCharacterAdId;
        public event Action MoreLoot1AdId;
        public event Action MoreLoot2AdId;
        public event Action AutoClickAdId;
        public event Action AutoMoneyPerClickAdId;

        public void Initialize()
        {
            YandexGame.RewardVideoEvent += OnRewardVideo;
        }

        public void ShowAd(int id)
        {
            Debug.Log($"Show ad with id: {AdRewardIds.GetAdRewardName(id)}");
            YandexGame.RewVideoShow(id);
        }

        private void OnRewardVideo(int id)
        {
            switch (id)
            {
                case 1:
                    OneAndHalfMoneyAdId?.Invoke();
                    break;
                case 2:
                    DoubleMoneyAdId?.Invoke();
                    break;
                case 3:
                    TripleMoneyAdId?.Invoke();
                    break;
                case 4:
                    Discount10AdId?.Invoke();
                    break;
                case 5:
                    Discount20AdId?.Invoke();
                    break;
                case 6:
                    Discount50AdId?.Invoke();
                    break;
                case 7:
                    BigCharacterAdId?.Invoke();
                    break;
                case 8:
                    SmallCharacterAdId?.Invoke();
                    break;
                case 9:
                    CrazyCharacterAdId?.Invoke();
                    break;
                case 10:
                    MoreLoot1AdId?.Invoke();
                    break;
                case 11:
                    MoreLoot2AdId?.Invoke();
                    break;
                case 12:
                    AutoClickAdId?.Invoke();
                    break;
                case 13:
                    AutoMoneyPerClickAdId?.Invoke();
                    break;
            }
        }
    }
}