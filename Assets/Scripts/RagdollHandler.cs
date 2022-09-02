using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Enemy))]
public class RagdollHandler : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;
    private Rigidbody[] _ragdollRigidbodies;

    private enum RagdollState
    {
        Enable,
        Disable
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();

        ChangeRagdollState(RagdollState.Disable);
    }

    private void OnEnable()
    {
        _enemy.Died += Enemy_OnDied;
    }

    private void OnDisable()
    {
        _enemy.Died -= Enemy_OnDied;
    }

    private void ChangeRagdollState(RagdollState ragdollState)
    {
        foreach (Rigidbody ragdollRigidbody in _ragdollRigidbodies)
        {
            ragdollRigidbody.isKinematic = ragdollState == RagdollState.Disable;
        }

        _animator.enabled = ragdollState == RagdollState.Disable;
    }

    private void Enemy_OnDied()
    {
        ChangeRagdollState(RagdollState.Enable);
    }
}