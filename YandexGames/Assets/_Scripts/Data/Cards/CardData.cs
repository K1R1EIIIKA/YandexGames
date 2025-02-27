using System;
using UnityEngine;

namespace _Scripts.Data.Cards
{
    [Serializable]
    public class CardData : IComparable
    {
        public string Id;
        public string Name;
        public Sprite Image;
        public int Cost;
        public Rarity Rarity;
        public bool IsOpen;
        public int MoneyPerClick;

        public CardData(CardObject cardObject)
        {
            Id = cardObject.Id;
            Name = cardObject.Name;
            Image = cardObject.Image;
            Cost = cardObject.Cost;
            Rarity = cardObject.Rarity;
            MoneyPerClick = cardObject.MoneyPerClick;
            IsOpen = false;
        }

        public CardData()
        {

        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, ImageName: {Image}, Cost: {Cost}, Rare: {Rarity}, IsOpen: {IsOpen}, MoneyPerClick: {MoneyPerClick}";
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is CardData otherCard)
            {
                if (Rarity == otherCard.Rarity)
                {
                    return Cost.CompareTo(otherCard.Cost);
                }

                return Rarity.CompareTo(otherCard.Rarity);
            }

            throw new ArgumentException("Object is not a CardData");
        }
    }
}