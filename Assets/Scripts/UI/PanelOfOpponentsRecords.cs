using UnityEngine;

[RequireComponent(typeof(RectMover))]
public class PanelOfOpponentsRecords : MonoBehaviour
{
    private RectMover _rectMover;

    private void Awake()
    {
        _rectMover = GetComponent<RectMover>();
    }

    public void MoveTo(Vector2 target, float requireTime)
    {
        _rectMover.MoveTo(target, requireTime);
    }
}
