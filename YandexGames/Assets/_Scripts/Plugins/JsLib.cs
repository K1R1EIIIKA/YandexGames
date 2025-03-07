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

        public void SetPlayerName(string name)
        {
            _accountController.SetPlayerName(name);
        }

        public void SetPlayerImage(string url)
        {
            _accountController.SetPlayerAvatar(url);
        }
    }
}