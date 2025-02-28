using System;

namespace _Scripts.Data.Cards
{
    [Serializable]
    public class PlayerCardData : CardData
    {
        public int Count;
        public int TotalMoneyPerClick => Count * ToCardObject().MoneyPerClick;

        public PlayerCardData(CardObject cardObject) : base(cardObject)
        {
            Count = 1;
        }

        public PlayerCardData(CardData cardData)
        {
            Id = cardData.Id;
            Name = cardData.Name;
            Image = cardData.Image;
            Cost = cardData.Cost;
            Rarity = cardData.Rarity;
            IsOpen = cardData.IsOpen;
            MoneyPerClick = cardData.MoneyPerClick;
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