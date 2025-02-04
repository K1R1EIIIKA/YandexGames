using System.IO;
using UnityEngine;

namespace _Scripts.Tools
{
    public static class SpriteExtensions
    {
        public static Sprite LoadSprite(string path)
        {
            if (!File.Exists(path)) return null;

            byte[] imageData = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);

            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}