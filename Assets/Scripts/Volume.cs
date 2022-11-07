using UnityEngine.UI;
using UnityEngine;
using Agava.WebUtility;

public class Volume : MonoBehaviour
{
    private const string VolumeKey = "Volume";

    [SerializeField] private Slider _slider;

    private float _value;// = 1f;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        WinCover.AdOpened += OnAdOpened;
        WinCover.AdClosed += OnAdClosed;
        Upgrading.AdOpened += OnAdOpened;
        Upgrading.AdClosed += OnAdClosed;
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        WinCover.AdOpened -= OnAdOpened;
        WinCover.AdClosed -= OnAdClosed;
        Upgrading.AdOpened -= OnAdOpened;
        Upgrading.AdClosed -= OnAdClosed;
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void Init()
    {
        _value = PlayerPrefs.GetFloat(VolumeKey, 1f);
        _slider.value = _value;
    }

    private void OnValueChanged(float value)
    {
        SetVolume(value);
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
        PlayerPrefs.SetFloat(VolumeKey, _value);
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
