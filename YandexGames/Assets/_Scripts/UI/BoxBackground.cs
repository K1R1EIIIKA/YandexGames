using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.UI
{
    public class BoxBackground : MonoBehaviour
    {
        [SerializeField] private BackgroundColor _backgroundColor;

        public BackgroundColor BackgroundColor => _backgroundColor;
    }
}