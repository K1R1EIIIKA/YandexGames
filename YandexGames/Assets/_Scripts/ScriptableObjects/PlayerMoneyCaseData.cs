using System;
using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [Serializable]
    public class PlayerMoneyCaseOpenedData
    {
        [SerializeField] private string caseObjectLocation;
        public string CaseObjectLocation => caseObjectLocation;

        public int OpenedCount;

        private const float PriceMultiplier = 0.1f;

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
                float price = basePrice;

                for (int i = 0; i < OpenedCount; i++)
                {
                    price += price * PriceMultiplier;
                }

                return price;
            }
        }


        public PlayerMoneyCaseOpenedData(MoneyCaseData caseData)
        {
            OpenedCount = 0;
            caseObjectLocation = caseData.name;
            Debug.Log("LOCATION " + caseObjectLocation);
        }

        private MoneyCaseData ToCaseData()
        {
            return Resources.Load<MoneyCaseData>("Cases/MoneyCases/" + caseObjectLocation);
        }

        public override string ToString()
        {
            return $"OpenedCount: {OpenedCount}, TotalPrice: {caseObjectLocation}";
        }
    }
}