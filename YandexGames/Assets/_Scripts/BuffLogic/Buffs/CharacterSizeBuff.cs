using UnityEngine;

namespace _Scripts.BuffLogic.Buffs
{
    public class CharacterSizeBuff : IBuff
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
    }
}