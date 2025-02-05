using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Tools
{
    public class CardSpritesLibrary
    {
        private const string CardSpritesFolderPath = "UI/Cards/Sprites";
        private static readonly Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

        public static Sprite LoadSprite(string name)
        {
            if (spriteCache.TryGetValue(name, out Sprite cachedSprite))
            {
                return cachedSprite;
            }

            Sprite loadedSprite = Resources.Load<Sprite>($"{CardSpritesFolderPath}/{name}");
            if (loadedSprite != null)
            {
                spriteCache[name] = loadedSprite;
            }
        
            return loadedSprite;
        }
    }
}