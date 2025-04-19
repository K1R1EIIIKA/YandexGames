using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewMoneyCase", menuName = "ScriptableObjects/Money Case")]
    public class MoneyCaseData : CaseData
    {
        public float Price;
        public bool IsTotalCases;
    }
}