using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private readonly int _idleToVictoryHash = Animator.StringToHash("IdleToVictory");

    private Animator _animator;

    public void RunIdleToVictory()
    {
        _animator.SetTrigger(_idleToVictoryHash);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}