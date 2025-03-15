using System;
using System.Runtime.InteropServices;
using _Scripts.Controllers;
using UnityEngine;

namespace _Scripts.Plugins
{
    public class JsLib : MonoBehaviour
    {
        [SerializeField] private AccountController _accountController;

        [DllImport("__Internal")]
        public static extern void GetPlayerData();
        [DllImport("__Internal")]
        private static extern IntPtr GetYandexLanguage();

        public static string GetLanguage()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                IntPtr stringPtr = GetYandexLanguage();
                if (stringPtr != IntPtr.Zero)
                {
                    string lang = Marshal.PtrToStringAuto(stringPtr);
                    return lang;
                }

                Debug.LogError("Не удалось получить язык из Yandex SDK.");
                return null;
            }

            Debug.LogWarning("Платформа не поддерживается.");
            return null;
        }

        public void SetPlayerName(string name)
        {
            _accountController.SetPlayerName(name);
        }

        public void SetPlayerImage(string url)
        {
            _accountController.SetPlayerAvatar(url);
        }

        private void Update()
        {
            Debug.Log(GetLanguage());
        }
    }
}