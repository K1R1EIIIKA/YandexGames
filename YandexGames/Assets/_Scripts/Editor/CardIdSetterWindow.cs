using System.Linq;
using UnityEditor;
using UnityEngine;
using _Scripts.Data.Cards;

public class CardIdSetterWindow : EditorWindow
{
    private string _prefix = "card_";

    [MenuItem("Tools/Card ID Setter")]
    public static void ShowWindow()
    {
        GetWindow<CardIdSetterWindow>("Card ID Setter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Настройки генерации ID", EditorStyles.boldLabel);

        _prefix = EditorGUILayout.TextField("Префикс ID:", _prefix);

        if (GUILayout.Button("Назначить ID всем картам"))
        {
            AssignIdsToCards();
        }
    }

    private void AssignIdsToCards()
    {
        string[] guids = AssetDatabase.FindAssets("t:CardObject");
        var cardObjects = guids.Select(guid =>
            AssetDatabase.LoadAssetAtPath<CardObject>(AssetDatabase.GUIDToAssetPath(guid))).ToList();

        if (cardObjects.Count == 0)
        {
            Debug.LogWarning("Не найдено ни одной карточки.");
            return;
        }

        Undo.RecordObjects(cardObjects.ToArray(), "Assign Card IDs");

        for (int i = 0; i < cardObjects.Count; i++)
        {
            cardObjects[i].Id = $"{_prefix}{i + 1}";
            EditorUtility.SetDirty(cardObjects[i]);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Назначены ID {cardObjects.Count} карточкам.");
    }
}