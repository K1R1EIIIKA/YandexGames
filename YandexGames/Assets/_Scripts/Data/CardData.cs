using System;

namespace _Scripts.Data
{
    [Serializable]
    public class CardData
    {
        public string Id;
        public string Name;
        public string ImagePath;
        public int Cost;
        public Rare Rare;
        public bool IsOpen;
        
        
    }

    public enum Rare
    {
        Common,
        Uncommon,
        SuperRare,
        SuperMegaRare,
        Advertisement,
    }
}