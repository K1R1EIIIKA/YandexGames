using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.Data.Cards
{
    public class CardData : ScriptableObject
    {
        public string Id;
        public string Name;
        public Sprite Image;
        public int Cost;
        [FormerlySerializedAs("Rare")] public Rarity Rarity;
        public bool IsOpen;

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, ImageName: {Image}, Cost: {Cost}, Rare: {Rarity}, IsOpen: {IsOpen}";
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