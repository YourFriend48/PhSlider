using UnityEngine;
using Agava.WebUtility;

public class Volume : MonoBehaviour
{
    private float _value = 1f;

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        WinCover.AdOpened += OnAdOpened;
        WinCover.AdClosed += OnAdClosed;
        Upgrading.AdOpened += OnAdOpened;
        Upgrading.AdClosed += OnAdClosed;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        WinCover.AdOpened -= OnAdOpened;
        WinCover.AdClosed -= OnAdClosed;
        Upgrading.AdOpened -= OnAdOpened;
        Upgrading.AdClosed -= OnAdClosed;
    }

    private void OnAdOpened()
    {
        SetVolume(0);
    }

    private void OnAdClosed()
    {
        SetVolume(1);
    }

    private void SetVolume(float value)
    {
        _value = value;
        AudioListener.volume = value;
    }

    private void OnInBackgroundChange(bool isInBackground)
    {
        if (isInBackground)
        {
            AudioListener.pause = false;
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.pause = true;
            AudioListener.volume = _value;
        }
    }
}
