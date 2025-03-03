
using System;
using UnityEngine;

namespace _Scripts.BuffLogic
{
    public class MonoTimer : MonoBehaviour
    {
        public static MonoTimer CreateInstance
        {
            get
            {
                GameObject obj = new GameObject("[TIMER]");
                return obj.AddComponent<MonoTimer>();
            }
        }

        public event Action OnCompleted;

        private bool _isEnabled;
        private float _timer = 0f;
        private float _targetTime;

        public void StartTimer(float targetTime)
        {
            _targetTime = targetTime;
            _isEnabled = true;
        }

        private void Update()
        {
            if (!_isEnabled)
            {
                return;
            }

            _timer += Time.deltaTime;

            if (_timer >= _targetTime)
            {
                _isEnabled = false;
                OnCompleted?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}