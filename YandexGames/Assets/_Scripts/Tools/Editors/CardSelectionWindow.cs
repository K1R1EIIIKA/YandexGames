using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using _Scripts.Data.Cards;

public class CardSelectionWindow : EditorWindow
{
    private static Rarity _selectedRarity;
    private static Action<List<CardData>> onCardsSelected;
    private List<CardData> availableCards = new();
    private List<CardData> selectedCards = new();
    private Vector2 scrollPos;

    private const float ElementHeight = 22f; // Высота одного элемента
    private const float MinHeight = 150f;
    private const float MaxHeight = 500f;

    public static void ShowWindow(Rarity rarity, Action<List<CardData>> callback)
    {
        _selectedRarity = rarity;
        onCardsSelected = callback;

        var window = GetWindow<CardSelectionWindow>($"Выбор {rarity} карточек");
        window.Init();
        window.Show();
    }

    private void Init()
    {
        LoadCards();
        AdjustWindowSize();
    }

    private void LoadCards()
    {
        availableCards = Resources.LoadAll<CardData>("Cards").Where(c => c.Rarity == _selectedRarity).ToList();
    }

    private void AdjustWindowSize()
    {
        int count = availableCards.Count;
        float dynamicHeight = Mathf.Clamp(count * ElementHeight + 100, MinHeight, MaxHeight); // Подстраиваем высоту
        minSize = maxSize = new Vector2(350, dynamicHeight);
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField($"Выбор {_selectedRarity} карточек", EditorStyles.boldLabel);

        if (GUILayout.Button("Создать новую карточку"))
        {
            CardCreationWindow.ShowWindow(_selectedRarity, newCard =>
            {
                LoadCards();
                selectedCards.Add(newCard);
                AdjustWindowSize(); // Пересчёт высоты после добавления новой карточки
            });
        }

        EditorGUILayout.Space();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        foreach (var card in availableCards)
        {
            bool isSelected = selectedCards.Contains(card);
            if (EditorGUILayout.ToggleLeft($"{card.Name} ({card.Id})", isSelected))
            {
                if (!isSelected) selectedCards.Add(card);
            }
            else
            {
                if (isSelected) selectedCards.Remove(card);
            }
        }
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Добавить выбранные"))
        {
            onCardsSelected?.Invoke(new List<CardData>(selectedCards));
            Close();
        }
    }
}
