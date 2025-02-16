using System;

namespace _Scripts.Data.Cards
{
    [Serializable]
    public class CardData
    {
        public string Id;
        public string Name;
        public string ImageName;
        public int Cost;
        public Rare Rare;
        public bool IsOpen;

        public CardData(string id, string name, string imageName, int cost, Rare rare, bool isOpen)
        {
            Id = id;
            Name = name;
            ImageName = imageName;
            Cost = cost;
            Rare = rare;
            IsOpen = isOpen;
        }

        public CardData()
        {
                
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, ImageName: {ImageName}, Cost: {Cost}, Rare: {Rare}, IsOpen: {IsOpen}";
        }
    }

    public enum Rare
    {
        Common,
        Uncommon,
        Epic,
        Legendary,
        Advertisement,
    }
}