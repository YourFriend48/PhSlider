using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _waitBeforeLaunchCameraDisable = .5f;
    [SerializeField] private CinemachineVirtualCamera _launchCamera;
    [SerializeField] private CinemachineVirtualCamera _mainCamera;

    private void OnEnable()
    {
        _player.Landed += Player_OnLanded;
        _player.SteppedLastHitPlatform += Player_OnSteppedLastHitPlatform;
        _player.SteppedFinishPlatform += Player_OnSteppedFinishPlatform;
    }

    private void OnDisable()
    {
        _player.Landed -= Player_OnLanded;
        _player.SteppedLastHitPlatform -= Player_OnSteppedLastHitPlatform;
        _player.SteppedFinishPlatform -= Player_OnSteppedFinishPlatform;
    }

    private IEnumerator DisableLaunchCamera()
    {
        yield return new WaitForSeconds(_waitBeforeLaunchCameraDisable);
        _launchCamera.enabled = false;
    }

    private void Player_OnLanded()
    {
        StartCoroutine(DisableLaunchCamera());
    }

    private void Player_OnSteppedFinishPlatform()
    {
        Time.timeScale = 1;

        _mainCamera.enabled = true;
    }

    private void Player_OnSteppedLastHitPlatform()
    {
        Time.timeScale = 0.3f;

        _mainCamera.enabled = false;
    }
}