using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private IntruderDetector _enemyDetector;
    [SerializeField] private AlarmSound _alarmSound;

    private readonly HashSet<Enemy> _intruders = new HashSet<Enemy>();

    private void OnEnable()
    {
        if (_enemyDetector == null)
        {
            return;
        }

        _enemyDetector.IntruderEntered += OnIntruderEntered;
        _enemyDetector.IntruderExited += OnIntruderExited;
    }

    private void OnDisable()
    {
        if (_enemyDetector == null)
        {
            return;
        }

        _enemyDetector.IntruderEntered -= OnIntruderEntered;
        _enemyDetector.IntruderExited -= OnIntruderExited;
    }

    private void OnIntruderEntered(Enemy enemy)
    {
        bool wasEmpty = _intruders.Count == 0;

        _intruders.Add(enemy);

        if (wasEmpty && _intruders.Count > 0)
        {
            _alarmSound.StartAlarm();
        }
    }

    private void OnIntruderExited(Enemy enemy)
    {
        _intruders.Remove(enemy);

        if (_intruders.Count == 0)
        {
            _alarmSound.StopAlarm();
        }
    }
}