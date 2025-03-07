using System;
using _Scripts.Data.Cards;
using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Data.Beds
{
    [Serializable]
    public class BedData : IComparable
    {
        public string Id;
        public string Name;
        public string Description;
        public Sprite BedImage;
        public int Price;
        public Rarity Rarity;
        public BedBuffType BuffType;
        public bool IsOpen;

        public string BedObjectLocation;

        public BedData(BedObject bedObject)
        {
            Id = bedObject.Id;
            Name = bedObject.Name;
            Description = bedObject.Description;
            BedImage = bedObject.BedImage;
            Rarity = bedObject.Rarity;
            Price = bedObject.Price;
            BuffType = bedObject.BuffType;
            IsOpen = false;

            BedObjectLocation = Resources.Load<BedObject>($"Beds/{bedObject.name}").name;
        }

        private BedData()
        {

        }

        public string GetDescription()
        {
            return ToBedObject().Description;
        }

        public BedObject ToBedObject()
        {
            return Resources.Load<BedObject>($"Beds/{BedObjectLocation}");
        }

        public BedData Copy()
        {
            return new BedData
            {
                Id = Id,
                Name = Name,
                BedImage = BedImage,
                Price = Price,
                Rarity = Rarity,
                BuffType = BuffType,
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