using System;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField, Min(1)] private int _initialSize = 20;

    private ObjectPool<Cube> _pool;

    public event Action<Vector3> CubeReturned;
    public int SpawnedCount => _pool != null ? _pool.SpawnedCount : 0;
    public int CreatedCount => _pool != null ? _pool.CreatedCount : 0;
    public int ActiveCount => _pool != null ? _pool.ActiveCount : 0;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(_cubePrefab, transform, _initialSize);
    }

    public Cube GetCube(Vector3 position)
    {
        return _pool.Get(position, UnityEngine.Random.rotation, cube => cube.Initialize(this));
    }

    public void ReturnCube(Cube cube)
    {
        _pool.Return(cube, returnedCube => CubeReturned?.Invoke(returnedCube.transform.position));
    }
}
