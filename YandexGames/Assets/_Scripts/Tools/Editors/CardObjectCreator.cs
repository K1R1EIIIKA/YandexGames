#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine.Localization;
using System.IO;
using _Scripts.Data.Cards;

public class CardObjectCreator : EditorWindow
{
    // Пути к папкам внутри Assets
    private string sourceFolder = "Assets/YourImageFolder";
    private string targetFolder = "Assets/YourCardObjectsFolder";
    private const string tableCollectionName = "Characters";

    [MenuItem("Tools/Card Object Creator")]
    public static void ShowWindow()
    {
        GetWindow<CardObjectCreator>("Card Object Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Настройки", EditorStyles.boldLabel);

        DrawFolderSelector(ref sourceFolder, "Папка с изображениями:", "Выберите папку с изображениями");
        DrawFolderSelector(ref targetFolder, "Папка для сохранения CardObject:", "Выберите целевую папку");

        if (GUILayout.Button("Создать объекты CardObject"))
        {
            CreateCardObjects();
        }
    }

    private void DrawFolderSelector(ref string folderPath, string label, string panelTitle)
    {
        GUILayout.Label(label);
        GUILayout.BeginHorizontal();
        folderPath = EditorGUILayout.TextField(folderPath);
        if (GUILayout.Button("Обзор", GUILayout.MaxWidth(75)))
        {
            string selected = EditorUtility.OpenFolderPanel(panelTitle, "Assets", "");
            if (!string.IsNullOrEmpty(selected))
            {
                if (selected.StartsWith(Application.dataPath))
                    folderPath = "Assets" + selected.Substring(Application.dataPath.Length);
                else
                    EditorUtility.DisplayDialog("Ошибка", "Папка должна находиться внутри Assets!", "OK");
            }
        }
        GUILayout.EndHorizontal();
    }

    private void CreateCardObjects()
    {
        if (!Directory.Exists(sourceFolder))
        {
            Debug.LogError("Исходная папка не найдена: " + sourceFolder);
            return;
        }
        if (!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
            AssetDatabase.Refresh();
        }

        // Получаем коллекцию строк для локализации
        var tableCollection = LocalizationEditorSettings.GetStringTableCollection(tableCollectionName);
        if (tableCollection == null)
        {
            Debug.LogError($"String Table Collection '{tableCollectionName}' не найдена.");
            return;
        }
        var sharedData = tableCollection.SharedData;

        string[] files = Directory.GetFiles(sourceFolder, "*.*", SearchOption.TopDirectoryOnly);
        int createdCount = 0;

        foreach (string file in files)
        {
            string ext = Path.GetExtension(file).ToLower();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg")
                continue;

            string id = Path.GetFileNameWithoutExtension(file);

            // Проверка существующего CardObject
            bool exists = false;
            foreach (string guid in AssetDatabase.FindAssets("t:CardObject", new[] { targetFolder }))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var existing = AssetDatabase.LoadAssetAtPath<CardObject>(path);
                if (existing != null && existing.Id == id)
                {
                    exists = true;
                    break;
                }
            }
            if (exists)
                continue;

            // Загружаем спрайт
            string assetPath = file.Replace(Application.dataPath, "Assets");
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            if (sprite == null)
            {
                Debug.LogWarning("Не удалось загрузить спрайт: " + assetPath);
                continue;
            }

            // Добавляем ключи в SharedData (если их нет)
            string keyName = id + "_Name";
            string keyDesc = id + "_Description";
            if (sharedData.GetEntry(keyName) == null)
                sharedData.AddKey(keyName);
            if (sharedData.GetEntry(keyDesc) == null)
                sharedData.AddKey(keyDesc);

            // Создаем сам объект
            var newCard = ScriptableObject.CreateInstance<CardObject>();
            newCard.Id = id;
            newCard.Image = sprite;
            newCard.Name = new LocalizedString { TableReference = tableCollection.TableCollectionName, TableEntryReference = keyName };
            newCard.Description = new LocalizedString { TableReference = tableCollection.TableCollectionName, TableEntryReference = keyDesc };

            string outPath = Path.Combine(targetFolder, id + ".asset");
            AssetDatabase.CreateAsset(newCard, outPath);
            createdCount++;
        }

        // Сохраняем изменения в таблице и ассетах
        EditorUtility.SetDirty(sharedData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Создано {createdCount} новых объектов CardObject.");
    }
}
#endif