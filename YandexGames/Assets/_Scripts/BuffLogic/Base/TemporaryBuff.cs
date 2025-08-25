using _Scripts.BuffLogic.Database;
using UnityEngine;

namespace _Scripts.BuffLogic.Base
{
    public class TemporaryBuff : IBuff
    {
        private readonly IBuffable _owner;
        private readonly IBuff    _coreBuff;
        private readonly float    _duration;
        private readonly MonoTimer _timer;

        private const string UiPrefabPath     = "Prefabs/BuffTimerUI";
        private const string CanvasPrefabPath = "Prefabs/BuffTimerCanvas";

        private static GameObject _canvasInstance;
        private BuffTimerUI _uiInstance;

        public TemporaryBuff(IBuffable owner, IBuff coreBuff, float duration)
        {
            _owner    = owner;
            _coreBuff = coreBuff;
            _duration = duration;

            _timer = MonoTimer.CreateInstance;
            _timer.OnCompleted += RemoveSelf;

            if (_canvasInstance == null)
            {
                var canvasPrefab = Resources.Load<GameObject>(CanvasPrefabPath);
                if (canvasPrefab == null)
                    Debug.LogError($"Не найден префаб Canvas по пути {CanvasPrefabPath}");
                else
                {
                    _canvasInstance = Object.Instantiate(canvasPrefab);
                    Object.DontDestroyOnLoad(_canvasInstance);
                }
            }

            var uiPrefab = Resources.Load<BuffTimerUI>(UiPrefabPath);
            if (uiPrefab != null && _canvasInstance != null)
            {
                _uiInstance = Object.Instantiate(uiPrefab, _canvasInstance.transform.GetChild(0));

                var identified = _coreBuff as IIdentifiedBuff;
                string buffId = identified != null ? identified.Id : "default";
                Sprite icon = BuffDatabase.Instance.GetIcon(buffId);

                _uiInstance.Initialize(_timer, icon, _duration);
            }
            else Debug.LogError($"Не удалось загрузить UI-префаб по пути {UiPrefabPath}");

            _timer.StartTimer(_duration);
        }

        public BuffStats ApplyBuff(BuffStats baseStats)
        {
            return _coreBuff.ApplyBuff(baseStats);
        }

        private void RemoveSelf()
        {
            _owner.RemoveBuff(this);

            if (_uiInstance != null)
                Object.Destroy(_uiInstance.gameObject);

            _timer.OnCompleted -= RemoveSelf;
        }
    }
}
