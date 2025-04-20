using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.UI.Buttons
{
    public class PointerAnimationBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private void OnEnable()
        {
            transform.localScale = Vector3.one;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(1.075f, 0.2f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, 0.2f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(0.95f, 0.1f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(1.1f, 0.1f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}