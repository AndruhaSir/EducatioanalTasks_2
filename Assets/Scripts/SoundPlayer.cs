using System.Collections;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private Coroutine _fadeCoroutine;

    public void PlaySoundSmoothly(AudioSource audioSource, float duration = 2f)
    {
        if (audioSource == null || duration <= 0f)
        {
            return;
        }

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        audioSource.volume = 0f;
        audioSource.Play();

        _fadeCoroutine = StartCoroutine(FadeIn(audioSource, duration));
    }

    public void StopSoundSmoothly(AudioSource audioSource, float duration = 2f)
    {
        if (audioSource == null || duration <= 0f)
        {
            return;
        }

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(FadeOut(audioSource, duration));
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float volumeStep = 1f / duration;

        while (audioSource.volume < 1f)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 1f, volumeStep * Time.deltaTime);
            yield return null;
        }

        audioSource.volume = 1f;
        _fadeCoroutine = null;
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float volumeStep = 1f / duration;

        while (audioSource.volume > 0f)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0f, volumeStep * Time.deltaTime);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
        _fadeCoroutine = null;
    }
}
