using UnityEditor;
using UnityEngine;
using _Scripts.Data.Cards;

[CustomEditor(typeof(CardObject))]
public class CardObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Отрисовываем стандартный инспектор

        CardObject cardObject = (CardObject)target;

        // Если у объекта есть картинка, устанавливаем её как иконку
        if (cardObject.Image != null)
        {
            Texture2D texture = cardObject.Image.texture;
            EditorGUIUtility.SetIconForObject(cardObject, texture);
        }
    }
}