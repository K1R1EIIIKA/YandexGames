using _Scripts.Data.Cards;
using UnityEngine;

namespace _Scripts.Data.Cases
{
    [CreateAssetMenu(fileName = "NewAdCase", menuName = "ScriptableObjects/Ad Case")]
    public class AdCaseData : CaseData
    {
        public int AdsCount;
    }
}