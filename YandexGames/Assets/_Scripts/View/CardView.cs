using _Scripts.Data.Cards;
using _Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.View
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image objectImage;
        // [SerializeField] private TMP_Text objectName;

        public void Initialize(Color color, Sprite sprite)
        {
            backgroundImage.color = color;
            objectImage.sprite = sprite;
            objectImage.color = Color.white;
            // objectName.text = name;
        }

        public void Initialize(CardData cardObject)
        {
            backgroundImage.color = cardObject.Rarity.ToHexColor().ToColor();
            objectImage.sprite = cardObject.Image;
            objectImage.color = Color.white;
            // objectName.text = cardObject.Name;
        }

        public void SetViewToClosed()
        {
            objectImage.color = Color.black;
            // objectName.text = "Не найдено";
        }
    }
}