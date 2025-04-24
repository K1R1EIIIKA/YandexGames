using _Scripts.BuffLogic;
using _Scripts.Data.Cards;
using _Scripts.Enums;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace _Scripts.Data.Backgrounds
{
    [CreateAssetMenu(fileName = "BackgroundData", menuName = "Data/BackgroundData", order = 0)]
    public class BackgroundObject : ScriptableObject
    {
        public string Id;
        public LocalizedString Name;
        public LocalizedString Description;
        public Sprite BackgroundImage;
        public int Price;
        public Rarity Rarity;
        public BackgroundBuffType BuffType;
    }
}