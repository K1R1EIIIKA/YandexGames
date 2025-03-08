using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewMoneyCase", menuName = "ScriptableObjects/Money Case")]
    public class MoneyCaseData : CaseData
    {
        public int Price;
        public bool IsTotalCases;
    }
}