using System.Collections;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private RectMover _rectMover;
    [SerializeField] private RectTransform _panel;
    [SerializeField] private float _delayBeforeHide = 5f;

    [SerializeField] private float _panelAnimationTime = 0.5f;

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    private void Awake()
    {
        _startPosition = _panel.anchoredPosition;
        _endPosition = new Vector2(_startPosition.x, _panel.sizeDelta.y / 2);
    }

    private void OnEnable()
    {
        _playerMovement.MovementEnabled += OnMovementEnable;
        _playerMovement.Swipped += OnSwipped;
    }

    private void OnDisable()
    {
        _playerMovement.MovementEnabled -= OnMovementEnable;
        _playerMovement.Swipped -= OnSwipped;
        _rectMover.Completed -= OnHideCompleted;
    }

    private void OnMovementEnable()
    {
        _rectMover.MoveTo(_endPosition, _panelAnimationTime);
    }

    private void OnSwipped()
    {
        _playerMovement.Swipped -= OnSwipped;
        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(_delayBeforeHide);
        _rectMover.Completed += OnHideCompleted;
        _rectMover.MoveTo(_startPosition, _panelAnimationTime);
    }

    private void OnHideCompleted()
    {
        _rectMover.Completed -= OnHideCompleted;
        gameObject.SetActive(false);
    }
}
