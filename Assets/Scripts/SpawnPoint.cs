using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    public Transform TargetTransform => _targetTransform;

    private void OnValidate()
    {
        if(_targetTransform == null)
        {
            Debug.LogWarning("Не все компоненты назначены", this);
        }
    }
}
