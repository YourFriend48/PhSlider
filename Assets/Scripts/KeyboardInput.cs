using UnityEngine;

[RequireComponent(typeof(Movement))]
public class KeyboardInput : MonoBehaviour
{
    private Movement _movement;

    private void Start()
    {
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _movement.MoveForward();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _movement.MoveBack();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _movement.MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _movement.MoveRight();
        }
    }
}