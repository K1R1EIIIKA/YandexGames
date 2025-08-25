using System;
using _Scripts.Data.Cards;
using _Scripts.Enums;
using UnityEngine;
using UnityEngine.Localization;

namespace _Scripts.Data.Beds
{
    [Serializable]
    public class BedData : IComparable
    {
        public string Id => ToBedObject().Id;
        public LocalizedString Name => ToBedObject().Name;
        public LocalizedString Description => ToBedObject().Description;
        public Sprite BedImage => ToBedObject().BedImage;
        public float Price => ToBedObject().Price;
        public Rarity Rarity => ToBedObject().Rarity;
        public BedBuffType BuffType => ToBedObject().BuffType;
        public bool IsOpen;

        public string BedObjectLocation;

        public BedData(BedObject bedObject)
        {
            IsOpen = false;

            BedObjectLocation = Resources.Load<BedObject>($"Beds/{bedObject.name}").name;
        }

        private BedData()
        {

        }

        public string GetName()
        {
            return ToBedObject().Name.GetLocalizedString();
        }

        public string GetDescription()
        {
            return ToBedObject().Description.GetLocalizedString();
        }

        public BedObject ToBedObject()
        {
            return Resources.Load<BedObject>($"Beds/{BedObjectLocation}");
        }

        public BedData Copy()
        {
            return new BedData
            {
                IsOpen = IsOpen,
                BedObjectLocation = BedObjectLocation
            };
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is BedData otherBed)
            {
                if (Rarity == otherBed.Rarity)
                {
                    return Price.CompareTo(otherBed.ToBedObject().Price);
                }

                return Rarity.CompareTo(otherBed.Rarity);
            }

            throw new ArgumentException("Object is not a CardData");
        }
    }
}