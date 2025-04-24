using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/BuffDefinition")]
public class BuffDefinition : ScriptableObject
{
    [Tooltip("Уникальный идентификатор (например, класс баффа или строка-ключ)")]
    public string buffId;

    [Tooltip("Иконка баффа для UI")]
    public Sprite icon;

    // при желании: название, описание, цвет, звуки и пр.
}
