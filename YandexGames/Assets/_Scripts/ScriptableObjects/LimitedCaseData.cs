using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLimitedCase", menuName = "ScriptableObjects/Limited Case")]
    public class LimitedCaseData : CaseData
    {
        public int Price;
    }
}