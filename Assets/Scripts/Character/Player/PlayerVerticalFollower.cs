using UnityEngine;

public class PlayerVerticalFollower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed = 4.5f;

    private bool _isStopFollow;

    private void OnEnable()
    {
        _player.Died += PlayerOnDied;
    }

    private void Update()
    {
        if (_isStopFollow)
        {
            return;
        }

        Vector3 targetPosition = _player.transform.position;
        targetPosition.z = 0;

        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        _player.Died -= PlayerOnDied;
    }

    private void PlayerOnDied()
    {
        _isStopFollow = true;
    }
}