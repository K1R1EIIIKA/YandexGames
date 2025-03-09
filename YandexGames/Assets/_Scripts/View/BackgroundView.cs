using _Scripts.Data.Beds;
using _Scripts.ScriptableObjects;
using _Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.View
{
    public class BackgroundView : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _objectImage;

        public void Initialize(BackgroundData backgroundData)
        {
            _backgroundImage.color = backgroundData.Rarity.ToHexColor().ToColor();
            _objectImage.sprite = backgroundData.ToBackgroundObject().BackgroundImage;
            _objectImage.color = Color.white;
        }

        public void SetViewToClosed()
        {
            _objectImage.color = Color.black;
        }
    }
}