using System;
using UnityEditor;
using UnityEngine;
using _Scripts.Data.Cards;
using UnityEngine.UI;

public class CardCreationWindow : EditorWindow
{
    private static Rarity _selectedRarity;
    private static Action<CardObject> onCardCreated;

    private string cardName = "Новая карточка";
    private Sprite image;
    private int cost = 10;

    private const float ElementHeight = 25f;
    private const float MinHeight = 100f;
    private const float MaxHeight = 300f;

    public static void ShowWindow(Rarity rarity, Action<CardObject> callback)
    {
        _selectedRarity = rarity;
        onCardCreated = callback;

        var window = GetWindow<CardCreationWindow>("Создание карточки");
        window.AdjustWindowSize();
        window.Show();
    }

    private void AdjustWindowSize()
    {
        int elementCount = 5; // Количество полей (Название, изображение, стоимость, доступность, кнопка)
        float dynamicHeight = Mathf.Clamp(elementCount * ElementHeight + 10, MinHeight, MaxHeight);
        minSize = maxSize = new Vector2(300, dynamicHeight);
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Создание новой карточки", EditorStyles.boldLabel);

        cardName = EditorGUILayout.TextField("Название", cardName);
        image = (Sprite)EditorGUILayout.ObjectField("Изображение", image, typeof(Sprite), false);
        cost = EditorGUILayout.IntField("Стоимость", cost);

        EditorGUILayout.Space();
        if (GUILayout.Button("Создать карточку"))
        {
            CreateCard();
        }
    }

    private void CreateCard()
    {
        CardObject newCard = CreateInstance<CardObject>();
        newCard.Id = Guid.NewGuid().ToString();
        newCard.Name = cardName;
        newCard.Image = image;
        newCard.Cost = cost;
        newCard.Rarity = _selectedRarity;

        string path = $"Assets/Resources/Cards/{cardName}.asset";
        AssetDatabase.CreateAsset(newCard, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Создана новая карточка: {cardName}");

        onCardCreated?.Invoke(newCard);
        Close();
    }
}
