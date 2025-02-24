using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.UI
{
    public class BoxBackgroundChanger : MonoBehaviour
    {
        [SerializeField] private BoxBackground[] _boxBackgrounds;

        public void ChangeBackground(BackgroundColor color)
        {
            foreach (var boxBackground in _boxBackgrounds)
            {
                boxBackground.gameObject.SetActive(boxBackground.BackgroundColor == color);
            }
        }
    }
}