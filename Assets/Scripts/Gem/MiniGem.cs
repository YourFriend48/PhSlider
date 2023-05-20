using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using System;

[RequireComponent(typeof(JumpMover))]
[RequireComponent(typeof(LerpMover))]
public class MiniGem : Poolable
{
    //[SerializeField] private float _jumpTime;
    [SerializeField] private float _flyTime;

    private JumpMover _jumpMover;
    private LerpMover _lerpMover;

    public event Action<MiniGem> JumpCompleted;
    public event Action<MiniGem> FlyCompleted;

    private void Awake()
    {
        _jumpMover = GetComponent<JumpMover>();
        _lerpMover = GetComponent<LerpMover>();
    }

    protected override void Disable()
    {
        _jumpMover.Completed -= OnJumpCompleted;
        _lerpMover.Completed -= OnFlyCompleted;
    }

    public void Jump(Vector3 target, float jumpTime)
    {
        _jumpMover.Completed += OnJumpCompleted;
        _jumpMover.MoveLerp(target, jumpTime);
    }

    public void Fly(Vector3 target)
    {
        _lerpMover.Completed += OnFlyCompleted;
        _lerpMover.MoveLerp(target, _flyTime);
    }

    private void OnJumpCompleted()
    {
        JumpCompleted?.Invoke(this);
    }

    private void OnFlyCompleted()
    {
        Debug.Log("OnFlyCompleted");
        FlyCompleted?.Invoke(this);
    }
}
