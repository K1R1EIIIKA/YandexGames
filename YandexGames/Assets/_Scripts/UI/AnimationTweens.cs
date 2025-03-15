using DG.Tweening;
using UnityEngine;

namespace _Scripts.UI
{
    public static class AnimationTweens
    {
        private static Tween _currentShakeTween;

        public static void HandleWrongTransform(Transform transform)
        {
            if (_currentShakeTween != null && _currentShakeTween.IsActive())
            {
                _currentShakeTween.Kill();
            }

            _currentShakeTween = transform.DOShakePosition(0.5f, 15, 20);
        }
    }
}