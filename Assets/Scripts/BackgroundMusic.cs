using System.Collections;
using UnityEngine;

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

    private IEnumerator WaitNewClip(float delay)
    {
        yield return new WaitForSeconds(delay);
        Play();
    }

    private void Play()
    {
        AudioClip audioClip = GetRandomClip();
        _audioSource.clip = audioClip;
        _audioSource.Play();
        StartCoroutine(WaitNewClip(audioClip.length + _delay));
    }

    private AudioClip GetRandomClip()
    {
        return _clips[Random.Range(0, _clips.Length)];
    }
}
