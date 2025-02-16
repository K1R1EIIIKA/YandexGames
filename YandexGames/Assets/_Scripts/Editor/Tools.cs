using UnityEditor;
using UnityEngine;

namespace _Scripts.Editor
{
    public static class Tools 
    {
        [MenuItem("Tools/Clear prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            Debug.Log("Prefs cleared");
        }
    }
}