using UnityEngine;
using Agava.WebUtility;

public class Volume : MonoBehaviour
{
    private const string VolumeKey = "Volume";

    private float _value;// = 1f;

    public static Volume Instance { get; private set; }

    public float Value => _value;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _value = PlayerPrefs.GetFloat(VolumeKey, 1);
        SetVolume(_value);

        DontDestroyOnLoad(gameObject);
    }

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

    private void Init()
    {
        _value = PlayerPrefs.GetFloat(VolumeKey, 1f);
    }

    private void OnValueChanged(float value)
    {
        SetVolume(value);
    }

    private void OnAdOpened()
    {
        VolumeOff();
    }

    private void OnAdClosed()
    {
        VolumeOn();
    }

    private void SetVolume(float value)
    {
        _value = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlaySound();
    }

    public void VolumeOn()
    {
        PlaySound();
    }

    public void VolumeOff()
    {
        StopPlaying();
    }

    private void PlaySound()
    {
        AudioListener.pause = false;
        AudioListener.volume = _value;
    }

    private void StopPlaying()
    {
        AudioListener.pause = true;
        AudioListener.volume = 0;
    }

    private void OnInBackgroundChange(bool isInBackground)
    {
        if (isInBackground)
        {
            StopPlaying();
        }
        else
        {
            PlaySound();
        }
    }
}
