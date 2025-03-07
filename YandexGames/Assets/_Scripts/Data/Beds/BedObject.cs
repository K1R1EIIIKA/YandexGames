using _Scripts.Data.Cards;
using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Data.Beds
{
    [CreateAssetMenu(fileName = "BedData", menuName = "Data/Beds/BedData")]
    public class BedObject : ScriptableObject
    {
        public string Id;
        public string Name;
        public string Description;
        public Sprite BedImage;
        public Rarity Rarity;
        public int Price;
        public BedBuffType BuffType;
    }
}