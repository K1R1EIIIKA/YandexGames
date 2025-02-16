using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Tools
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField] private List<TKey> keys = new();
        [SerializeField] private List<TValue> values = new();

        private Dictionary<TKey, TValue> _dictionary = new();

        public Dictionary<TKey, TValue> ToDictionary()
        {
            _dictionary.Clear();
            for (int i = 0; i < keys.Count; i++)
            {
                if (i < values.Count)
                {
                    _dictionary[keys[i]] = values[i];
                }
            }
            return _dictionary;
        }

        public void FromDictionary(Dictionary<TKey, TValue> dict)
        {
            keys.Clear();
            values.Clear();
            foreach (var kvp in dict)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }
    }
}