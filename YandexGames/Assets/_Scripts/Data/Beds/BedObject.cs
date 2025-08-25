using _Scripts.Data.Cards;
using _Scripts.Enums;
using UnityEngine;
using UnityEngine.Localization;

namespace _Scripts.Data.Beds
{
    [CreateAssetMenu(fileName = "BedData", menuName = "Data/Beds/BedData")]
    public class BedObject : ScriptableObject
    {
        public string Id;
        public LocalizedString Name;
        public LocalizedString Description;
        public Sprite BedImage;
        public Rarity Rarity;
        public float Price;
        public BedBuffType BuffType;
    }
}