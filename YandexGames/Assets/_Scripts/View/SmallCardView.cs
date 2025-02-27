using _Scripts.Data.Cards;
using _Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.View
{
    public class SmallCardView : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image objectImage;
        [SerializeField] private TextMeshProUGUI objectCount;

        public void Initialize(CardData cardObject)
        {
            backgroundImage.color = cardObject.Rarity.ToHexColor().ToColor();
            objectImage.sprite = cardObject.ToCardObject().Image;
            objectImage.color = Color.white;

            if (cardObject is PlayerCardData playerCardData)
            {
                objectCount.gameObject.SetActive(true);
                objectCount.text = playerCardData.Count.ToString();
            }
            else
            {
                objectCount.gameObject.SetActive(false);
            }
        }

        public void SetViewToClosed()
        {
            objectImage.color = Color.black;
        }
    }
}