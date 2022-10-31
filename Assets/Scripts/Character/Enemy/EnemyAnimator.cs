using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private readonly int _idleToVictoryHash = Animator.StringToHash("IdleToVictory");
    private const string Kick = "Kick";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemy.Struck += OnStruck;
    }

    private void OnDisable()
    {
        _enemy.Struck -= OnStruck;
    }

    private void OnStruck()
    {
        _animator.SetTrigger(Kick);
    }

    public void RunIdleToVictory()
    {
        _animator.SetTrigger(_idleToVictoryHash);
    }
}