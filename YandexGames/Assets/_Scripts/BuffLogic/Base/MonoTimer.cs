using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.BuffLogic.Base
{
    public class MonoTimer : MonoBehaviour
    {
        public static MonoTimer CreateInstance
        {
            get
            {
                GameObject obj = new GameObject("[TIMER]");
                DontDestroyOnLoad(obj);
                return obj.AddComponent<MonoTimer>();
            }
        }

        /// <summary>Вызывается каждый кадр, пока таймер работает, с прогрессом от 0 до 1.</summary>
        public event Action<float> OnTick;
        public event Action OnCompleted;

        private bool _isEnabled;
        private float _elapsed;
        private float _targetTime;

        public void StartTimer(float targetTime)
        {
            _targetTime = targetTime;
            _elapsed = 0f;
            _isEnabled = true;
        }

        private void Update()
        {
            if (!_isEnabled || SceneManager.GetActiveScene().name != "MainScreen")
                return;

            _elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(_elapsed / _targetTime);
            OnTick?.Invoke(progress);
            // Debug.Log(_elapsed);

            if (_elapsed >= _targetTime)
            {
                _isEnabled = false;
                OnCompleted?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}