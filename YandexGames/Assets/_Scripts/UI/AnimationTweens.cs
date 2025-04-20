using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.UI
{
    public static class AnimationTweens
    {
        private static readonly Dictionary<Transform, Vector3> _originalLocalPositions = new();

        private static readonly Dictionary<Transform, Tween> _currentShakeTweens = new();

        public static void HandleWrongTransform(Transform t)
        {
            if (!_originalLocalPositions.ContainsKey(t))
                _originalLocalPositions[t] = t.localPosition;

            if (_currentShakeTweens.TryGetValue(t, out var oldTween) && oldTween.IsActive())
            {
                oldTween.Kill();
                t.localPosition = _originalLocalPositions[t];
            }

            var shakeTween = t.DOShakePosition(
                duration: 0.5f,
                strength: 15f,
                vibrato: 20,
                randomness: 90,
                fadeOut: true
            )
            .SetLink(t.gameObject, LinkBehaviour.KillOnDestroy)
            .OnKill(() => t.localPosition = _originalLocalPositions[t]);

            _currentShakeTweens[t] = shakeTween;
        }
    }
}
