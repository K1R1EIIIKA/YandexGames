using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewAdCase", menuName = "ScriptableObjects/Ad Case")]
    public class AdCaseData : CaseData
    {
        public int AdsCount;
        public bool IsTotalAdsCount;
    }
}