using System;
using System.Collections.Generic;
using _Scripts.Data.Cards;
using UnityEngine;

namespace _Scripts.Data.Cases
{
    [Serializable]
    public abstract class CaseData
    {
        public string Id;
        public string Name;
        public List<CardData> CardPool;
        public CaseType CaseType;

        protected CaseData(string id, string name, List<CardData> cardPool, CaseType caseType)
        {
            Id = id;
            Name = name;
            CardPool = cardPool;
            CaseType = caseType;
        }

        public virtual CardData OpenCase()
        {
            if (CardPool == null || CardPool.Count == 0)
            {
                Debug.LogError($"Кейс {Name} пуст!");
                return null;
            }

            return CardPool[UnityEngine.Random.Range(0, CardPool.Count)];
        }
    }

    public enum CaseType
    {
        Money,
        Advertisement
    }
}