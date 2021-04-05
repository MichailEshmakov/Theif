using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaler : MonoBehaviour
{
    [SerializeField] private float _volumeChangingSpeed;
    [Range(0, 1)] [SerializeField] private float _maxVolume; 

    private AudioSource _audioSource;
    private bool _isPlaying = false;

    private void OnValidate()
    {
        _volumeChangingSpeed = Mathf.Clamp01(_volumeChangingSpeed);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    public void StartPlaying()
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        _isPlaying = true;
        StartCoroutine(IncreaseVolume());
    }

    public void StopPlaying()
    {
        _isPlaying = false;
        StartCoroutine(DecreaseVolume());
    }

    private IEnumerator IncreaseVolume()
    {
        while (_isPlaying && _audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _volumeChangingSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        while (_isPlaying == false && _audioSource.volume > 0)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 0, _volumeChangingSpeed * Time.deltaTime);
            yield return null;
        }

        if (_isPlaying == false && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
