using UnityEngine;
using System;

public class SwipeInput : MonoBehaviour
{
    private Vector3 _startPosition;

    public event Action<Vector2Int> Swipped;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition != _startPosition)
            {
                Vector2Int direction = SelectDirection();
                Swipped?.Invoke(direction);
            }
        }
    }

    private Vector2Int SelectDirection()
    {
        float verticalDistance = Mathf.Abs(Input.mousePosition.y - _startPosition.y);
        float horizontalDistance = Mathf.Abs(Input.mousePosition.x - _startPosition.x);

        return horizontalDistance > verticalDistance ? SelectHorizontalDirection() : SelectVerticalDirection();
    }

    private Vector2Int SelectHorizontalDirection()
    {
        return Input.mousePosition.x > _startPosition.x ? Vector2Int.right : Vector2Int.left;
    }

    private Vector2Int SelectVerticalDirection()
    {
        return Input.mousePosition.y > _startPosition.y ? Vector2Int.up : Vector2Int.down;
    }
}
