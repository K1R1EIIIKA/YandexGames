using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using _Scripts.Data.Cards;
using _Scripts.Data.Cases;
using _Scripts.Enums;
using _Scripts.Tools;
using Unity.VisualScripting;

public class CaseCreationWindow : EditorWindow
{
    private string caseName = "Новый кейс";
    private Dictionary<Rarity, List<CardObject>> selectedCards = new();
    private Dictionary<Rarity, float> dropChances = new();
    private Vector2 scrollPos;

    private int _moneyCasePrice;
    private int _adCaseAdCount;
    private CaseType _caseType;

    [MenuItem("Tools/Создать новый кейс")]
    public static void ShowWindow()
    {
        GetWindow<CaseCreationWindow>("Создание кейса").Show();
    }

    private void OnEnable()
    {
        foreach (Rarity rare in System.Enum.GetValues(typeof(Rarity)))
        {
            selectedCards[rare] = new List<CardObject>();
            dropChances[rare] = 0;
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Создание нового кейса", EditorStyles.boldLabel);
        caseName = EditorGUILayout.TextField("Название кейса", caseName);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Тип кейса", EditorStyles.boldLabel);
        _caseType = (CaseType) EditorGUILayout.EnumPopup("Тип кейса", _caseType);

        if (_caseType == CaseType.Money)
        {
            _moneyCasePrice = EditorGUILayout.IntField("Цена кейса", _moneyCasePrice);
        }
        else if (_caseType == CaseType.Ad)
        {
            _adCaseAdCount = EditorGUILayout.IntField("Количество реклам", _adCaseAdCount);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Выбор карточек по редкости", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(300));
        foreach (Rarity rare in System.Enum.GetValues(typeof(Rarity)))
        {
            if (rare == Rarity.Special) continue;

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(rare.ToString(), EditorStyles.boldLabel);

            if (selectedCards[rare].Count > 0)
            {
                foreach (var card in selectedCards[rare])
                {
                    EditorGUILayout.LabelField($"- {card.Name} ({card.Id})");
                }
            }
            else
            {
                EditorGUILayout.LabelField("Нет карточек", EditorStyles.miniLabel);
            }

            if (GUILayout.Button($"Добавить {rare} карточки"))
            {
                CardSelectionWindow.ShowWindow(rare, cards => selectedCards[rare] = cards);
            }

            if (selectedCards[rare].Count > 0)
            {
                dropChances[rare] = EditorGUILayout.FloatField("Шанс выпадения (%)", dropChances[rare]);
            }
            else
            {
                dropChances[rare] = 0;
                EditorGUILayout.LabelField("Шанс: 0% (нет карт)", EditorStyles.miniLabel);
            }

            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        if (GUILayout.Button("Создать кейс"))
        {
            CreateCase();
        }
    }

    private void CreateCase()
    {
        if (_caseType == CaseType.Money && _moneyCasePrice == 0)
        {
            Debug.LogError("Цена кейса не может быть равна 0");
            return;
        }

        if (_caseType == CaseType.Ad && _adCaseAdCount == 0)
        {
            Debug.LogError("Количество реклам не может быть равно 0");
            return;
        }

        if (selectedCards.Values.All(c => c.Count == 0))
        {
            Debug.LogError("Кейс должен содержать хотя бы одну карточку");
            return;
        }

        if (dropChances.Values.All(c => c == 0))
        {
            Debug.LogError("Шанс выпадения карточек не может быть равен 0");
            return;
        }

        if (_caseType == CaseType.Money)
        {
            CreateMoneyCase();
        }
        else if (_caseType == CaseType.Ad)
        {
            CreateAdCase();
        }
    }

    private void CreateMoneyCase()
    {
        var newCase = CreateInstance<MoneyCaseData>();
        newCase.Price = _moneyCasePrice;
        newCase.Name = caseName;
        newCase.CardPool = selectedCards.Values.SelectMany(c => c).ToList();
        // newCase.DropChancesSerializable = new List<SerializableDictionaryItem<Rarity, float>>();
        foreach (var kvp in dropChances)
        {
            // newCase.DropChancesSerializable.AddRange(
        }

        string path = $"Assets/Resources/Cases/{caseName}.asset";
        AssetDatabase.CreateAsset(newCase, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Создан новый кейс: {caseName}");
        Close();
    }

    private void CreateAdCase()
    {
        var newCase = CreateInstance<AdCaseData>();
        newCase.AdsCount = _adCaseAdCount;
        newCase.Name = caseName;
        newCase.CardPool = selectedCards.Values.SelectMany(c => c).ToList();
        // newCase.DropChancesSerializable =

        string path = $"Assets/Resources/Cases/{caseName}.asset";
        AssetDatabase.CreateAsset(newCase, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Создан новый кейс: {caseName}");
        Close();
    }
}
