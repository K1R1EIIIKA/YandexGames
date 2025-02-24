using System;
using UnityEngine;

namespace _Scripts.Tools
{
    public class InputManager : MonoBehaviour
    {
        public static event Action OnMouseClick; // Событие клика

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // ЛКМ
            {
                OnMouseClick?.Invoke();
            }
        }
    }
}