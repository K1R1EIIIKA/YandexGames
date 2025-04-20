using System;
using _Scripts.BuffLogic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _Scripts.UI.Buttons
{
    public class CharacterButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _moveStrength = 124.7f;
        [SerializeField] private float _smoothness = 10f;

        [SerializeField] private GameObject _floatingImagePrefab;
        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private Vector2 _randomXRange = new(-300f, 300f);
        [SerializeField] private Vector2 _randomYRange = new(0, 0);
        [SerializeField] private Vector2 _randomPowerRange = new(200f, 500f);

        private Tween _tween;
        private BuffController _buffController;

        private float _autoClicksInterval = 0.1f;
        private float _autoClicksTime = 0;

        [Inject]
        public void Construct(BuffController buffController)
        {
            _buffController = buffController;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _tween?.Complete(true);

            transform.parent.DOScale(Vector3.one * 1.1f, 0.3f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
            _tween = transform.DOPunchScale(Vector3.one * 0.05f, .7f, 5, 2f).SetEase(Ease.InSine)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tween?.Complete(true);

            transform.parent.DOScale(Vector3.one, 0.3f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
            _tween = transform.DOPunchScale(Vector3.one * -0.05f, .7f, 5, 2f).SetEase(Ease.InSine)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            _tween?.Complete(true);

            transform.parent.DOScale(Vector3.one, 0.15f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
            _tween = transform.DOPunchScale(Vector3.one * 0.05f, .5f, 8, 2f).SetEase(Ease.InSine)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _tween?.Complete(true);

            transform.parent.DOScale(Vector3.one * 1.1f, 0.15f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
            _tween = transform.DOPunchScale(Vector3.one * -0.05f, .5f, 8, 2f).SetEase(Ease.InSine)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SpawnFloatingImage(eventData.position);
        }

        private void SpawnFloatingImage(Vector3 clickPosition)
        {
            if (_floatingImagePrefab == null) return;

            GameObject floatingImage = Instantiate(_floatingImagePrefab, clickPosition, Quaternion.identity, transform);

            float randomX = Random.Range(_randomXRange.x, _randomXRange.y);
            float randomY = Random.Range(_randomYRange.x, _randomYRange.y);
            float randomPower = Random.Range(_randomPowerRange.x, _randomPowerRange.y);
            float randomRotation = Random.Range(0, 360);

            Vector3 targetPosition = clickPosition + new Vector3(randomX, randomY, 0);

            floatingImage.transform.DOJump(targetPosition, randomPower, 1, 1.75f).SetEase(Ease.OutSine).SetLink(floatingImage, LinkBehaviour.KillOnDestroy);
            floatingImage.transform.DORotate(new Vector3(0, 0, randomRotation), 1.75f, RotateMode.FastBeyond360).SetLink(floatingImage, LinkBehaviour.KillOnDestroy);
            floatingImage.GetComponent<Image>().DOFade(0, 0.75f).SetDelay(0.75f).SetLink(floatingImage, LinkBehaviour.KillOnDestroy)
                .OnComplete(() => Destroy(floatingImage, 1f));
        }

        private void Update()
        {
            if (_buffController.CurrentStats.IsAutoClick){
                _autoClicksTime += Time.deltaTime;
                if (_autoClicksTime >= _autoClicksInterval)
                {
                    SpawnFloatingImage(transform.position);
                    _autoClicksTime = 0;
                }
            }
        }
    }
}