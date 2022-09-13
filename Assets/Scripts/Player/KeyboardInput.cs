using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class KeyboardInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        var inputDirection = new Vector3(Input.GetAxisRaw("Vertical") * -1, 0, Input.GetAxisRaw("Horizontal"));

        if (inputDirection.magnitude > 0)
        {
            _playerMovement.Move(inputDirection);
        }
    }
}