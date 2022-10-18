using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerComponents
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Map _map;
        private SwipeInput _swipeInput;
        private Vector2Int _lastDirection;

        public event Action Reached;

        private void OnEnable()
        {
            _swipeInput.Swipped += OnSwipped;
        }

        private void OnDisable()
        {
            _swipeInput.Swipped -= OnSwipped;
            Reached -= OnReached;
        }

        public void Init(Map map, SwipeInput swipeInput)
        {
            _map = map;
            Cell cell = _map.GetCell(_map.PlayerPosition);
            transform.position = cell.transform.position;
            _swipeInput = swipeInput;
            _swipeInput.enabled = true;
            enabled = true;
        }

        private void OnSwipped(Vector2Int direction)
        {
            Vector2Int nextPosition = _map.PlayerPosition + direction;
            Cell cell = _map.GetCell(nextPosition);

            if (cell.Type != Cell.WalkableType.Unwalkable)
            {
                _lastDirection = direction;
                _map.SetPlayerPosition(nextPosition);
                _swipeInput.enabled = false;
                Reached += OnReached;
                StartCoroutine(Moving(cell.transform.position));
                //MoveStepByStep(direction);                
                //вычислить конечную точку движени€ -- ј нафига?
                //но двигатьс€ на одну клетку
            }
            else
            {
                // StopMoving
                _swipeInput.enabled = true;
            }
        }

        private void OnReached()
        {
            Reached -= OnReached;
            OnSwipped(_lastDirection);
        }

        private IEnumerator Moving(Vector3 target)
        {
            while (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
                yield return null;
            }

            Reached?.Invoke();
        }
    }
}
