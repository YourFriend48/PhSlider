using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Movement _movement;
    [SerializeField] private float _waitBeforeLaunchCameraDisable = 0.5f;
    [SerializeField] private CinemachineVirtualCamera _launchCamera;
    [SerializeField] private CinemachineVirtualCamera _mainCamera;

    private void OnEnable()
    {
        _player.Landed += Player_OnLanded;
        _movement.LastHitInitiated += Movement_OnLastHitInitiated;
        _movement.FinishReached += Movement_OnFinishReached;
    }

    private void OnDisable()
    {
        _player.Landed -= Player_OnLanded;
        _movement.LastHitInitiated -= Movement_OnLastHitInitiated;
        _movement.FinishReached -= Movement_OnFinishReached;
    }

    private IEnumerator DisableLaunchCamera()
    {
        yield return new WaitForSeconds(_waitBeforeLaunchCameraDisable);
        _launchCamera.enabled = false;
    }

    private void Movement_OnFinishReached()
    {
        _mainCamera.enabled = true;
    }

    private void Movement_OnLastHitInitiated()
    {
        _mainCamera.enabled = false;
    }

    private void Player_OnLanded()
    {
        StartCoroutine(DisableLaunchCamera());
    }
}