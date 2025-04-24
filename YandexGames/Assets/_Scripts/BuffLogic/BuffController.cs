using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BuffLogic.Base;
using UnityEngine;

namespace _Scripts.BuffLogic
{
    public class BuffController : IBuffable
    {
        public BuffStats BaseStats { get; private set; }
        public BuffStats CurrentStats { get; private set; }

        private readonly List<IBuff> _buffs = new();

        public Action OnBuffsChanged { get; set; }

        public void Initialize(BuffStats stats)
        {
            BaseStats = stats;
            CurrentStats = BaseStats;
        }

        public void AddBuff(IBuff buff)
        {
            _buffs.Add(buff);
            ApplyBuffs();

            Debug.Log($"Buff added: {buff.GetType().Name}");
        }

        public void RemoveBuff(IBuff buff)
        {
            _buffs.Remove(buff);
            ApplyBuffs();

            Debug.Log($"Buff removed: {buff.GetType().Name}");
        }

        public void RemoveBuffByType<T>() where T : IBuff
        {
            var buffsToRemove = _buffs.Where(buff => buff is T).ToList();
            foreach (var buff in buffsToRemove)
            {
                RemoveBuff(buff);
            }
        }

        private void ApplyBuffs()
        {
            CurrentStats = BaseStats.Copy();

            foreach (var buff in _buffs)
            {
                CurrentStats = buff.ApplyBuff(CurrentStats);
            }

            OnBuffsChanged?.Invoke();
        }
    }
}