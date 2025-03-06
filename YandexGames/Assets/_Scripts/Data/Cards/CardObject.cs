using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Data.Cards
{
    public class CardObject : ScriptableObject
    {
        public string Id;
        public string Name;
        public string Description;
        public Sprite Image;
        public int Cost;
        [FormerlySerializedAs("Rare")] public Rarity Rarity;
        public int MoneyPerClick;

        public CardData ToCardData()
        {
            return new CardData(this);
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, ImageName: {Image}, Cost: {Cost}, Rare: {Rarity}, MoneyPerClick: {MoneyPerClick}";
        }
    }

    [Serializable]
    public enum Rarity
    {
        Common,
        Rare,
        SuperRare,
        SuperMegaRare,
        Special,
    }
}