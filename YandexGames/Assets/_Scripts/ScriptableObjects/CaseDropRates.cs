using System;
using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Data.Cards;
using _Scripts.Tools;
using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CaseDropRates", menuName = "ScriptableObjects/CaseDropRates", order = 1)]
    public class CaseDropRates : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<Rare, int> serializedDictionary = new();

        private Dictionary<Rare, int> _dictionary;

        public Dictionary<Rare, int> GetDictionary()
        {
            return _dictionary ??= serializedDictionary.ToDictionary();
        }

        public void SetDictionary(Dictionary<Rare, int> newDictionary)
        {
            serializedDictionary.FromDictionary(newDictionary);
        }
    }
}