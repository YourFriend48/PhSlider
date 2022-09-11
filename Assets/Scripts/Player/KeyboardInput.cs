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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _playerMovement.MoveForward();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _playerMovement.MoveBack();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _playerMovement.MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _playerMovement.MoveRight();
        }
    }
}