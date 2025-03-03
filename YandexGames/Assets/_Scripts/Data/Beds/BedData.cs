using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Data.Beds
{
    [CreateAssetMenu(fileName = "BedData", menuName = "Data/Beds/BedData")]
    public class BedData : ScriptableObject
    {
        public string Id;
        public string Name;
        public Sprite BedImage;
        public int Price;
        public BedBuffType BuffType;
    }
}