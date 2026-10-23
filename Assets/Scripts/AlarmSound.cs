using System.Collections;
using UnityEngine;

public class AlarmSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField, Min(0.01f)] private float _fadeDuration = 2f;

    private Coroutine _fadeCoroutine;

    public void StartAlarm()
    {
        if (_audioSource == null)
        {
            return;
        }

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _audioSource.volume = 0f;
        _audioSource.Play();

        _fadeCoroutine = StartCoroutine(ChangeVolume(1f));
    }

    public void StopAlarm()
    {
        if (_audioSource == null)
        {
            return;
        }

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(ChangeVolume(0f));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        float volumeStep = 1f / _fadeDuration;

        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, volumeStep * Time.deltaTime);
            yield return null;
        }

        _audioSource.volume = targetVolume;

        if (targetVolume == 0f)
        {
            _audioSource.Stop();
        }

        _fadeCoroutine = null;
    }
}