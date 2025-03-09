using System;
using UnityEngine;
using YG;

namespace _Scripts.YG
{
    public class AdRewardController
    {
        public event Action<float> OneAndHalfMoneyAdId;
        public event Action<float> DoubleMoneyAdId;
        public event Action<float> TripleMoneyAdId;
        public event Action<float> Discount10AdId;
        public event Action<float> Discount20AdId;
        public event Action<float> Discount50AdId;
        public event Action<float> BigCharacterAdId;
        public event Action<float> SmallCharacterAdId;
        public event Action<float> CrazyCharacterAdId;
        public event Action<float> MoreLoot1AdId;
        public event Action<float> MoreLoot2AdId;
        public event Action<float> AutoClickAdId;
        public event Action<int> AutoMoneyPerClickAdId;
        public event Action AdCaseAdId;

        private Action _onAdWatched;

        public void Initialize()
        {
            YandexGame.RewardVideoEvent += OnRewardVideo;
        }

        public void ShowAd(int id, Action callback = null)
        {
            _onAdWatched = callback;
            YandexGame.RewVideoShow(id);
        }

        private void OnRewardVideo(int id)
        {
            OnRewardVideo(id, true);
            _onAdWatched?.Invoke();
            _onAdWatched = null;
        }

        public void OnRewardVideo(int id, bool isAdd)
        {
            switch (id)
            {
                case 1:
                    OneAndHalfMoneyAdId?.Invoke(isAdd ? 6f : 1f);
                    break;
                case 2:
                    DoubleMoneyAdId?.Invoke(isAdd ? 6f : 1f);
                    break;
                case 3:
                    TripleMoneyAdId?.Invoke(isAdd ? 6f : 1f);
                    break;
                case 4:
                    Discount10AdId?.Invoke(isAdd ? 6f : 1f);
                    break;
                case 5:
                    Discount20AdId?.Invoke(isAdd ? 6f : 1f);
                    break;
                case 6:
                    Discount50AdId?.Invoke(isAdd ? 2f : 1f);
                    break;
                case 7:
                    BigCharacterAdId?.Invoke(isAdd ? 6f : 1f);
                    break;
                case 8:
                    SmallCharacterAdId?.Invoke(isAdd ? 6f : 1f);
                    break;
                case 9:
                    CrazyCharacterAdId?.Invoke(isAdd ? 6f : 1f);
                    break;
                case 10:
                    MoreLoot1AdId?.Invoke(isAdd ? 6f : 1f);
                    break;
                case 11:
                    MoreLoot2AdId?.Invoke(isAdd ? 3f : 1f);
                    break;
                case 12:
                    AutoClickAdId?.Invoke(isAdd ? 6f : 1f);
                    break;
                case 13:
                    AutoMoneyPerClickAdId?.Invoke(isAdd ? 6 : 1);
                    break;
                case 14:
                    AdCaseAdId?.Invoke();
                    break;
            }
        }

        public string GetBuffText(int id)
        {
            switch (id)
            {
                case 1:
                    return "1.5x money";
                case 2:
                    return "2x money";
                case 3:
                    return "3x money";
                case 4:
                    return "10% discount";
                case 5:
                    return "20% discount";
                case 6:
                    return "50% discount";
                case 7:
                    return "Big character";
                case 8:
                    return "Small character";
                case 9:
                    return "Crazy character";
                case 10:
                    return "More loot 1";
                case 11:
                    return "More loot 2";
                case 12:
                    return "Auto click";
                case 13:
                    return "Auto money per click";
                default:
                    return "Unknown buff";
            }
        }
    }
}