
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SoundPlayer _soundPlayer;

    private int _countIntruders;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _countIntruders++;

            if (_countIntruders == 1)
            {
                _soundPlayer.PlaySoundSmoothly(_audioSource);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _countIntruders--;

            if (_countIntruders == 0)
            {
                _soundPlayer.StopSoundSmoothly(_audioSource);
            }
        }
    }
}
