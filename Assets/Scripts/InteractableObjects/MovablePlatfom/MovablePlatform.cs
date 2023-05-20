using UnityEngine;

[RequireComponent(typeof(LerpMover))]
public class MovablePlatform : MonoBehaviour
{
    public int PositionIndex;

    [SerializeField] private float _movingTime = 1f;

    private LerpMover _LerpMover;


    private void Awake()
    {
        _LerpMover = GetComponent<LerpMover>();
    }

    public void Move(Vector3 target)
    {
        _LerpMover.MoveLerp(target, _movingTime);
    }
}
