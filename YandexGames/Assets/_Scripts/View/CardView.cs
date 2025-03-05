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
        [SerializeField] private TMP_Text objectName;
        [SerializeField] private Material _specialMaterial;

        public void Initialize(CardData cardObject)
        {
            if (cardObject.Rarity == Rarity.Special)
            {
                backgroundImage.color = Color.white;
                backgroundImage.material = _specialMaterial;
            }
            else
            {
                objectImage.material = null;
                backgroundImage.color = cardObject.Rarity.ToHexColor().ToColor();
            }
            objectImage.sprite = cardObject.ToCardObject().Image;
            objectImage.color = Color.white;
            objectName.text = cardObject.Name;
        }

        public void SetViewToClosed()
        {
            objectImage.color = Color.black;
            objectName.text = "Не найдено";
        }
    }
}