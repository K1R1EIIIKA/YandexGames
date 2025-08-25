using System;
using _Scripts.Controllers;
using UnityEngine;
using Zenject;

namespace _Scripts.Data.Cards
{
    [Serializable]
    public class PlayerCardData : CardData
    {
        public int Count;

        [Inject] private TransactionController _transactionController;

        public float TotalMoneyPerClick
        {
            get
            {
                if (Rarity == Rarity.Special)
                { 
                    var highestprice = _transactionController.GetHighestPriceCard();
                    return highestprice;
                }

                float[] multipliers = { 1, 1, 2, 2.9f, 3.7f, 4.4f, 4.9f, 5.3f, 5.6f, 5.8f, 6.0f };
                float baseMoneyPerClick = ToCardObject().MoneyPerClick;

                float multiplier = multipliers[Math.Min(Count, multipliers.Length - 1)];

                return baseMoneyPerClick * multiplier;
            }
        }

        public new float MoneyPerClick
        {
            get
            {
                if (Rarity == Rarity.Special)
                {
                    var highestprice = _transactionController.GetHighestPriceCard();
                    return highestprice;
                }

                return ToCardObject().MoneyPerClick;
            }
        }

        public PlayerCardData(CardObject cardObject) : base(cardObject)
        {
            Count = 1;
        }

        public PlayerCardData(CardData cardData)
        {
            Id = cardData.Id;
            Name = cardData.Name;
            Image = cardData.Image;
            IsOpen = cardData.IsOpen;
            CardObjectLocation = cardData.CardObjectLocation;
            Count = 1;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, ImageName: {Image}, Cost: {Cost}, Rare: {Rarity}, IsOpen: {IsOpen}, " +
                   $"Count: {Count}, MoneyPerClick: {MoneyPerClick}";
        }
    }
}