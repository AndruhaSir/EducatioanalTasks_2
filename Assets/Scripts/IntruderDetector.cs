using System;
using UnityEngine;

public class IntruderDetector : MonoBehaviour
{
    public event Action<Enemy> IntruderEntered;
    public event Action<Enemy> IntruderExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            IntruderEntered?.Invoke(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            IntruderExited?.Invoke(enemy);
        }
    }
}