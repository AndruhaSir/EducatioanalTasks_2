using UnityEngine;

public class BombPool : MonoBehaviour
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField, Min(1)] private int _initialSize = 10;

    private ObjectPool<Bomb> _pool;

    public int SpawnedCount => _pool.SpawnedCount;
    public int CreatedCount => _pool.CreatedCount;
    public int ActiveCount => _pool.ActiveCount;

    private void Awake()
    {
        _pool = new ObjectPool<Bomb>(_bombPrefab, transform, _initialSize);
    }

    public Bomb GetBomb(Vector3 position)
    {
        return _pool.Get(position, Quaternion.identity, bomb => bomb.Initialize(this));
    }

    public void ReturnBomb(Bomb bomb)
    {
        _pool.Return(bomb, null);
    }
}