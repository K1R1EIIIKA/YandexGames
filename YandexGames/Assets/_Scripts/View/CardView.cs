using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.View
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image objectImage;
        [SerializeField] private TMP_Text objectName;

        public void Initialize(Color color, Sprite sprite, string name)
        {
            backgroundImage.color = color;
            objectImage.sprite = sprite;
            objectImage.color = Color.white;
            objectName.text = name;
        }
    }
}