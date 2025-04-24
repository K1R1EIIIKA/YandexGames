using UnityEngine;
using UnityEngine.UI;
using _Scripts.BuffLogic.Base;

namespace _Scripts.BuffLogic
{
    public class BuffTimerUI : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Image _iconImage;

        private MonoTimer _timer;

        /// <summary>
        /// Инициализация: подписываемся на таймер и задаём иконку.
        /// </summary>
        public void Initialize(MonoTimer timer, Sprite buffIcon, float duration)
        {
            _iconImage.sprite = buffIcon;
            _fillImage.fillAmount = 1f;

            _timer = timer;
            // Подписываемся на каждый кадр
            _timer.OnTick += UpdateFill;
            // Когда закончится — отписаться и уничтожить UI
            _timer.OnCompleted += Cleanup;
            DontDestroyOnLoad(gameObject);
        }

        private void UpdateFill(float progress)
        {
            // progress от 0 до 1: нужно обратное
            _fillImage.fillAmount = 1f - progress;
        }

        private void Cleanup()
        {
            if (_timer != null)
            {
                _timer.OnTick -= UpdateFill;
                _timer.OnCompleted -= Cleanup;
            }
            Destroy(gameObject);
        }
    }
}