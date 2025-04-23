using System;
using System.Collections;
using UnityEngine;

public class RandomButtonService : MonoBehaviour
{
    public static RandomButtonService Instance { get; private set; }

    [Header("Интервалы (сек.)")]
    [SerializeField] private float _minInterval = 5f;
    [SerializeField] private float _maxInterval = 10f;
    [SerializeField] private float _visibleDuration = 5f;

    public event Action OnShow;
    public event Action OnHide;

    public bool IsShow { get; private set; }

    private Coroutine _loop;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _loop = StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        while (true)
        {
            // ждем перед показом
            yield return new WaitForSeconds(UnityEngine.Random.Range(_minInterval, _maxInterval));
            OnShow?.Invoke();
            IsShow = true;
            Debug.Log("Show random button");

            // ждем пока кнопка видна
            yield return new WaitForSeconds(_visibleDuration);
            OnHide?.Invoke();
            IsShow = false;
            Debug.Log("Hide random button");
        }
    }

    private void OnDestroy()
    {
        if (_loop != null) StopCoroutine(_loop);
    }
}