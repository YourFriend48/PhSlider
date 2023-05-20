using UnityEngine;

[RequireComponent(typeof(TowardsMover))]
public class IoIoMover : MonoBehaviour
{
    private TowardsMover _towardsMover;
    private Vector3 _startPosition;

    private void Awake()
    {
        _towardsMover = GetComponent<TowardsMover>();
        _startPosition = transform.localPosition;
    }

    private void OnDisable()
    {
        _towardsMover.Completed -= OnComleted;
    }

    public void Move(Vector3 translation)
    {
        _towardsMover.MoveTowards(translation);
        _towardsMover.Completed += OnComleted;
    }

    private void OnComleted()
    {
        _towardsMover.Completed -= OnComleted;
        _towardsMover.MoveTowards(_startPosition);
    }
}
