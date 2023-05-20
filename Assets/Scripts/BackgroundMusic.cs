using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private float _delay;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Play();
    }

    private IEnumerator WaitNewClip()
    {
        while (AudioListener.pause || _audioSource.isPlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(_delay);
        Play();
    }

    private void Play()
    {
        AudioClip audioClip = GetRandomClip();
        _audioSource.clip = audioClip;
        _audioSource.Play();
        StartCoroutine(WaitNewClip());
    }

    private AudioClip GetRandomClip()
    {
        return _clips[Random.Range(0, _clips.Length)];
    }
}
