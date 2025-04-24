using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BuffLogic;
using _Scripts.Data.Backgrounds;
using _Scripts.Data.Beds;
using _Scripts.Data.Cards;
using _Scripts.Tools;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace _Scripts.ScriptableObjects
{
    [Serializable]
    public class BackgroundData : IComparable
    {
        public string Id;
        public LocalizedString Name;
        public LocalizedString Description;
        public Sprite BedImage;
        public int Price;
        public Rarity Rarity;
        public BackgroundBuffType BuffType;
        public bool IsOpen;

        public string BackgroundObjectLocation;

        public BackgroundData(BackgroundObject backgroundObject)
        {
            Id = backgroundObject.Id;
            Name = backgroundObject.Name;
            Description = backgroundObject.Description;
            BedImage = backgroundObject.BackgroundImage;
            Rarity = backgroundObject.Rarity;
            Price = backgroundObject.Price;
            BuffType = backgroundObject.BuffType;
            IsOpen = false;

            BackgroundObjectLocation = Resources.Load<BackgroundObject>($"Backgrounds/{backgroundObject.name}").name;
        }

        private BackgroundData()
        {

        }

        public string GetName()
        {
            return ToBackgroundObject().Name.GetLocalizedString();
        }

        public string GetDescription()
        {
            return ToBackgroundObject().Description.GetLocalizedString();
        }

        public BackgroundObject ToBackgroundObject()
        {
            return Resources.Load<BackgroundObject>($"Backgrounds/{BackgroundObjectLocation}");
        }

        public BackgroundData Copy()
        {
            return new BackgroundData
            {
                Id = Id,
                Name = Name,
                Description = Description,
                BedImage = BedImage,
                Rarity = Rarity,
                Price = Price,
                BuffType = BuffType,
                IsOpen = IsOpen,
                BackgroundObjectLocation = BackgroundObjectLocation
            };
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is BackgroundData otherBed)
            {
                if (Rarity == otherBed.Rarity)
                {
                    return Price.CompareTo(otherBed.ToBackgroundObject().Price);
                }

                return Rarity.CompareTo(otherBed.Rarity);
            }

            throw new ArgumentException("Object is not a BackgroundData");
        }
    }
}
