using UnityEngine;
using System;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectMover))]
public class PanelOfUpgrades : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _panelAnimationTime = 1f;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private RectMover _rectMover;
    private Vector2 _startPosition;
    private Vector2 _endPosition;

    public event Action Completed;

    private void Awake()
    {
        _rectMover = GetComponent<RectMover>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _startPosition = _rectTransform.anchoredPosition;
        _endPosition = new Vector2(_startPosition.x, _rectTransform.sizeDelta.y / 2);
    }

    private void Start()
    {
        Appear();
    }

    private void OnDisable()
    {
        _playerMovement.Swipped -= OnSwipped;
        Completed -= OnDisapearCompleted;
    }

    private void Appear()
    {
        _playerMovement.Swipped += OnSwipped;
        _rectMover.MoveTo(_endPosition, _panelAnimationTime);
    }

    private void OnSwipped()
    {
        _playerMovement.Swipped -= OnSwipped;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _rectMover.Completed += OnDisapearCompleted;
        _rectMover.MoveTo(_startPosition, _panelAnimationTime);
    }

    private void OnDisapearCompleted()
    {
        Completed -= OnDisapearCompleted;
        gameObject.SetActive(false);
    }
}
