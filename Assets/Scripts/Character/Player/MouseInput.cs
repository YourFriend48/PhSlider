using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class MouseInput : MonoBehaviour
{
    private Vector3 _mousePreviousPosition;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePreviousPosition = Input.mousePosition;
        }

        Vector3 mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonUp(0) == false || mousePosition == _mousePreviousPosition)
        {
            return;
        }
        
        if (Mathf.Abs(mousePosition.x - _mousePreviousPosition.x)
            > Mathf.Abs(mousePosition.y - _mousePreviousPosition.y))
        {
            if (mousePosition.x > _mousePreviousPosition.x)
            {
                _playerMovement.MoveRight();
            }
            else
            {
                _playerMovement.MoveLeft();
            }
        }
        else
        {
            if (mousePosition.y > _mousePreviousPosition.y)
            {
                _playerMovement.MoveForward();
            }
            else
            {
                _playerMovement.MoveBack();
            }
        }
    }
}