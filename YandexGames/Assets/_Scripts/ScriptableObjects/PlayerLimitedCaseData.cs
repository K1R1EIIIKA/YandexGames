using System;
using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [Serializable]
    public class PlayerLimitedCaseOpenedData
    {
        [SerializeField] private string caseObjectLocation;
        public string CaseObjectLocation => caseObjectLocation;

        public int OpenedCount;

        private const float PriceMultiplier = 10f;

        public string Id => ToCaseData().Id;
        public float TotalPrice
        {
            get
            {
                if (OpenedCount == 0)
                {
                    return ToCaseData().Price;
                }

                float basePrice = ToCaseData().Price;
                for (int i = 0; i < OpenedCount; i++)
                {
                    basePrice *= PriceMultiplier;
                }

                return basePrice;
            }
        }


        public PlayerLimitedCaseOpenedData(LimitedCaseData caseData)
        {
            OpenedCount = 0;
            caseObjectLocation = caseData.name;
            Debug.Log("LOCATION " + caseObjectLocation);
        }

        private LimitedCaseData ToCaseData()
        {
            return Resources.Load<LimitedCaseData>("Cases/LimitedCases/" + caseObjectLocation);
        }

        public override string ToString()
        {
            return $"OpenedCount: {OpenedCount}, TotalPrice: {caseObjectLocation}";
        }
    }
}