using System;
using UnityEngine;
using UnityEngine.Localization;

namespace _Scripts.Data.Cards
{
    [Serializable]
    public class CardData : IComparable
    {
        public string Id;
        public LocalizedString Name;
        public LocalizedString Description;
        public Sprite Image;
        public int Cost => ToCardObject().Cost;
        public Rarity Rarity => ToCardObject().Rarity;
        public bool IsOpen;
        public int MoneyPerClick => ToCardObject().MoneyPerClick;

        public string CardObjectLocation;

        public CardData(CardObject cardObject)
        {
            Id = cardObject.Id;
            Name = cardObject.Name;
            Description = cardObject.Description;
            Image = cardObject.Image;
            IsOpen = false;

            // location of the card object
            CardObjectLocation = Resources.Load<CardObject>("Cards/"+cardObject.name).name;
        }

        public CardObject ToCardObject()
        {
            // Debug.Log("Cards/"+CardObjectLocation);
            return Resources.Load<CardObject>("Cards/"+CardObjectLocation);
        }

        public string GetName()
        {
            return ToCardObject().Name.GetLocalizedString();
        }

        public string GetDescription()
        {
            return ToCardObject().Description.GetLocalizedString();
        }

        public CardData()
        {

        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, ImageName: {Image}, Cost: {Cost}, Rare: {Rarity}, IsOpen: {IsOpen}, MoneyPerClick: {MoneyPerClick}, " +
                   $"CardObjectLocation: {CardObjectLocation}";
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is CardData otherCard)
            {
                if (Rarity == otherCard.Rarity)
                {
                    return Cost.CompareTo(otherCard.ToCardObject().Cost);
                }

                return Rarity.CompareTo(otherCard.Rarity);
            }

            throw new ArgumentException("Object is not a CardData");
        }
    }
}