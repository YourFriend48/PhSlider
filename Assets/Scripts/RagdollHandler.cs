using UnityEngine;

[RequireComponent(typeof(Animator), typeof(ICharacter))]
public class RagdollHandler : MonoBehaviour
{
    private Animator _animator;
    private ICharacter _character;
    private Rigidbody[] _ragdollRigidbodies;

    private enum RagdollState
    {
        Enable,
        Disable
    }

    private void Awake()
    {
        _character = GetComponent<ICharacter>();
        _animator = GetComponent<Animator>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();

        ChangeRagdollState(RagdollState.Disable);
    }

    private void OnEnable()
    {
        _character.Died += CharacterOnDied;
    }

    private void OnDisable()
    {
        _character.Died -= CharacterOnDied;
    }

    private void ChangeRagdollState(RagdollState ragdollState)
    {
        if (_character as Player && ragdollState == RagdollState.Disable)
        {
            return;
        }

        foreach (Rigidbody ragdollRigidbody in _ragdollRigidbodies)
        {
            ragdollRigidbody.isKinematic = ragdollState == RagdollState.Disable;
        }

        _animator.enabled = ragdollState == RagdollState.Disable;
    }

    private void CharacterOnDied()
    {
        ChangeRagdollState(RagdollState.Enable);
    }
}