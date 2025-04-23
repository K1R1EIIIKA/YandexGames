using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Infrastructure.Core.SceneTransitions
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup Curtain;

        // private void Awake()
        // {
        //     DontDestroyOnLoad(this);
        // }

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
        }

        public void Hide()
        {
            Curtain.DOFade(0, 0.1f).SetDelay(0.2f).SetLink(gameObject, LinkBehaviour.KillOnDestroy).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}