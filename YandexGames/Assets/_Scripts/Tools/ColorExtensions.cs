using UnityEngine;

namespace _Scripts.Tools
{
    public static class ColorExtensions
    {
        public static Color ToColor(this string colorString)
        {
            if (ColorUtility.TryParseHtmlString(colorString, out Color color))
            {
                return color;
            }

            Debug.LogWarning($"Ошибка: '{colorString}' не является корректным HEX-кодом цвета.");
            return Color.white;
        }

        public static string ToHexString(this Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }
    }
}