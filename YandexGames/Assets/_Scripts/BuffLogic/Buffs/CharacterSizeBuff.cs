using _Scripts.BuffLogic.Base;
using UnityEngine;

namespace _Scripts.BuffLogic.Buffs
{
    public class CharacterSizeBuff : IIdentifiedBuff
    {
        private readonly float _sizeMultiplier;
        private readonly bool _isCrazy;

        public CharacterSizeBuff(float sizeMultiplier, bool isCrazy)
        {
            _sizeMultiplier = sizeMultiplier;
            _isCrazy = isCrazy;
        }

        public BuffStats ApplyBuff(BuffStats stats)
        {
            var statsCopy = stats.Copy();

            statsCopy.CharacterScale *= _sizeMultiplier;
            statsCopy.IsCharacterSizeChanged = true;
            statsCopy.IsCharacterSizeCrazy = _isCrazy;

            return statsCopy;
        }

        public string Id
        {
            get
            {
                if (_isCrazy)
                {
                    return "sizecrazy";
                }

                return _sizeMultiplier > 1 ? "sizeup" : "sizedown";
            }
        }
    }
}