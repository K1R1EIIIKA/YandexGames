using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Data.Cards;
using _Scripts.Tools;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Scripts.Data.Cases
{
    [Serializable]
    public abstract class CaseData : ScriptableObject
    {
        public string Id;
        public string Name;
        public Sprite CaseImage;
        public Vector2Int LootCountRange;
        public Vector2Int CoinsRange;
        public List<CardData> CardPool;

        [SerializedDictionary("Rarity", "Drop Chance")]
        public SerializedDictionary<Rarity, float> DropChancesSerializedDictionary = new();
        public Dictionary<Rarity, float> DropChances => DropChancesSerializedDictionary.ToDictionary();

        public int GetRandomLootCount()
        {
            return UnityEngine.Random.Range(LootCountRange.x, LootCountRange.y + 1);
        }

        public int GetRandomCoins()
        {
            return UnityEngine.Random.Range(CoinsRange.x, CoinsRange.y + 1);
        }

        public CardData GetRandomCard()
        {
            LogDropChances();
            if (CardPool == null || CardPool.Count == 0 || DropChances == null || DropChances.Count == 0)
                return null;

            // Создаём список карт с их шансами
            List<(CardData card, float chance)> weightedCards = new();

            foreach (var card in CardPool)
            {
                if (DropChances.TryGetValue(card.Rarity, out float chance))
                {
                    weightedCards.Add((card, chance));
                }
            }

            if (weightedCards.Count == 0)
                return null;

            // Считаем общий вес
            float totalWeight = weightedCards.Sum(c => c.chance);
            float randomValue = UnityEngine.Random.Range(0, totalWeight);

            // Выбираем карту по весу
            float cumulativeWeight = 0;
            foreach (var (card, chance) in weightedCards)
            {
                cumulativeWeight += chance;
                if (randomValue <= cumulativeWeight)
                    return card;
            }

            return weightedCards.Last().card; // Защита от ошибок (если вдруг не выберется)
        }

        private void LogDropChances()
        {
            if (DropChances == null || DropChances.Count == 0)
            {
                Debug.LogWarning($"CaseData {name} has no drop chances");
                return;
            }

            foreach (var (rarity, chance) in DropChances)
            {
                Debug.Log($"CaseData {name} has drop chance {chance} for rarity {rarity}");
            }
        }
    }
}
