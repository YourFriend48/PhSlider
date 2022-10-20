using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class PanelOfUpgrades : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MouseInput _mouseInput;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Coroutine _moving;

    public event Action Completed;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Appear();
    }

    private void OnEnable()
    {
        _mouseInput.Swipped += OnSwipped;
    }

    private void OnDisable()
    {
        _mouseInput.Swipped -= OnSwipped;
        Completed -= OnDisapearCompleted;
    }

    private void Appear()
    {
        Vector2 target = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.sizeDelta.y / 2);

        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MoveTo(target));
    }

    private void OnSwipped()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        Vector2 target = new Vector2(_rectTransform.anchoredPosition.x, -_rectTransform.sizeDelta.y / 2);
       
        if(_moving!=null)
        {
            StopCoroutine(_moving);
        }

        Completed += OnDisapearCompleted;
        _moving = StartCoroutine(MoveTo(target));
    }

    private void OnDisapearCompleted()
    {
        Completed -= OnDisapearCompleted;
        gameObject.SetActive(false);
    }

    private IEnumerator MoveTo(Vector2 target)
    {
        while(_rectTransform.anchoredPosition != target)
        {
        _rectTransform.anchoredPosition = Vector2.MoveTowards(_rectTransform.anchoredPosition,target, _speed * Time.deltaTime);
            yield return null;
        }

        Completed?.Invoke();
    }
}
