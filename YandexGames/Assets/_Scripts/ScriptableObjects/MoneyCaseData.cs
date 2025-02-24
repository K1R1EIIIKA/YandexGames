using System.Collections.Generic;
using _Scripts.Data.Cards;
using UnityEngine;

namespace _Scripts.Data.Cases
{
    [CreateAssetMenu(fileName = "NewMoneyCase", menuName = "ScriptableObjects/Money Case")]
    public class MoneyCaseData : CaseData
    {
        public int Price;
    }
}