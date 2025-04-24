using _Scripts.Data.Beds;
using _Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.View
{
    public class BedView : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _objectImage;

        public void Initialize(BedData bedData)
        {
            _backgroundImage.color = bedData.Rarity.ToHexColor().ToColor();
            _objectImage.sprite = bedData.BedImage;
            _objectImage.color = Color.white;
        }

        public void SetViewToClosed()
        {
            _objectImage.color = Color.black;
        }
    }
}