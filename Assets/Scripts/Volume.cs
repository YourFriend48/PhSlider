using UnityEngine;
using Agava.WebUtility;

public class Volume : MonoBehaviour
{
    private const string VolumeKey = "Volume";

    private float _value;// = 1f;
    private bool _isAdOpen = false;

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
        _value = PlayerPrefs.GetFloat(VolumeKey, 1);
    }

    private void Start()
    {
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
        InvulnerabilityView.AdOpened += OnAdOpened;
        InvulnerabilityView.AdClosed += OnAdClosed;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        WinCover.AdOpened -= OnAdOpened;
        WinCover.AdClosed -= OnAdClosed;
        Upgrading.AdOpened -= OnAdOpened;
        Upgrading.AdClosed -= OnAdClosed;
        InvulnerabilityView.AdOpened -= OnAdOpened;
        InvulnerabilityView.AdClosed -= OnAdClosed;
    }

    private void OnAdOpened()
    {
        _isAdOpen = true;
        AudioListener.volume = 0;
        AudioListener.pause = IsZero(AudioListener.volume);
    }

    private void OnAdClosed()
    {
        _isAdOpen = false;
        AudioListener.volume = _value;
        AudioListener.pause = IsZero(AudioListener.volume);
    }

    private void SetVolume(float value)
    {
        _value = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlaySound();
    }

    public void VolumeOn()
    {
        SetVolume(_value);
    }

    public void VolumeOff()
    {
        SetVolume(0);
    }

    private void PlaySound()
    {
        AudioListener.volume = _value;
        AudioListener.pause = IsZero(AudioListener.volume);
    }

    private void OnInBackgroundChange(bool isInBackground)
    {
        if (isInBackground)
        {
            AudioListener.volume = 0;
            AudioListener.pause = IsZero(AudioListener.volume);
        }
        else
        {
            if (_isAdOpen)
            {
                AudioListener.volume = 0;
                AudioListener.pause = IsZero(AudioListener.volume);
            }
            else
            {
                AudioListener.volume = _value;
                AudioListener.pause = IsZero(AudioListener.volume);
            }
        }
    }

    private bool IsZero(float value)
    {
        return value == 0;
    }
}
