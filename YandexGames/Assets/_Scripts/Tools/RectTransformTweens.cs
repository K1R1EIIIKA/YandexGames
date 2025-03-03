using System.Collections.Generic;
using _Scripts.BuffLogic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Scripts.Tools
{
    public static class RectTransformTweens
    {
        public static void DestroyAllChildren(this RectTransform rectTransform)
        {
            for (int i = rectTransform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(rectTransform.GetChild(i).gameObject);
            }
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this SerializedDictionary<TKey, TValue> serializedDictionary)
        {
            Dictionary<TKey, TValue> dictionary = new();
            foreach (var pair in serializedDictionary)
            {
                dictionary.Add(pair.Key, pair.Value);
            }

            return dictionary;
        }
    }
}