using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Data.Cards;
using _Scripts.Enums;
using _Scripts.Tools;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Localization;
using Random = UnityEngine.Random;

namespace _Scripts.ScriptableObjects
{
    [Serializable]
    public abstract class CaseData : ScriptableObject
    {
        public string Id;
        public LocalizedString Name;
        public Sprite CaseImage;
        public Vector2Int LootCountRange;
        public Vector2 CoinsRange;
        public List<CardObject> CardPool;
        public CaseTier Tier;

        [SerializedDictionary("Rarity", "Drop Chance")]
        public SerializedDictionary<Rarity, float> DropChancesSerializedDictionary = new();
        public Dictionary<Rarity, float> DropChances => DropChancesSerializedDictionary.ToDictionary();

        public int GetRandomLootCount()
        {
            //log path to file
            Debug.Log($"CaseData {name} has loot count range {LootCountRange.x} - {LootCountRange.y}");
            return Random.Range(LootCountRange.x, LootCountRange.y + 1);
        }

        public string GetName()
        {
            return Name.GetLocalizedString();
        }

        public float GetRandomCoins()
        {
            return Random.Range(CoinsRange.x, CoinsRange.y + 1);
        }

        public CardData GetRandomCard()
        {
            if (CardPool == null || CardPool.Count == 0 || DropChances == null || DropChances.Count == 0)
                return null;

            float totalRarityWeight = DropChances.Values.Sum();
            float rand = Random.Range(0f, totalRarityWeight);
            float cum = 0f;
            Rarity selectedRarity = Rarity.Common;

            foreach (var kv in DropChances)
            {
                cum += kv.Value;
                if (rand <= cum)
                {
                    selectedRarity = kv.Key;
                    break;
                }
            }

            var cardsOfRarity = CardPool.Where(c => c.Rarity == selectedRarity).ToList();
            if (cardsOfRarity.Count == 0)
                return null;

            int idx = Random.Range(0, cardsOfRarity.Count);
            return cardsOfRarity[idx].ToCardData();
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
