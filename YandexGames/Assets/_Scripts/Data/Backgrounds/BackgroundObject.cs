using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Data.Backgrounds
{
    [CreateAssetMenu(fileName = "BackgroundData", menuName = "Data/BackgroundData", order = 0)]
    public class BackgroundObject : ScriptableObject
    {
        public string Id;
        public string Name;
        public Sprite BackgroundImage;
        public int Cost;
        public BackgroundBuffType BuffType;
    }
}