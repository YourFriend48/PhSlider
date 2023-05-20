using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    [SerializeField] private LerpIoIoMover _lerpIoIoMover;
    [SerializeField] private float _offset;

    private Vector3 _endPosition;

    private void Awake()
    {
    }

    public void Enable()
    {
        _endPosition = transform.position + _lerpIoIoMover.transform.forward * _offset;
        _lerpIoIoMover.Completed += OnCicleCompleted;
        _lerpIoIoMover.Move(_endPosition);
    }

    private void OnDisable()
    {
        _lerpIoIoMover.Completed -= OnCicleCompleted;
    }

    private void OnCicleCompleted()
    {
        _lerpIoIoMover.Move(_endPosition);
    }
}
