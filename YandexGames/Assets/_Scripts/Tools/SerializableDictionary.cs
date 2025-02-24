using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Tools
{
    [Serializable]
    public class SerializableDictionaryItem<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public SerializableDictionaryItem()
        {
        }

        public SerializableDictionaryItem(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}