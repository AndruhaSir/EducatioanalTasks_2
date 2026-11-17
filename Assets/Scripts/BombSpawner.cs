using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private BombPool _bombPool;

    protected override int SpawnedCount => _bombPool.SpawnedCount;
    protected override int CreatedCount => _bombPool.CreatedCount;
    protected override int ActiveCount => _bombPool.ActiveCount;
    protected override int StatisticsOrder => 1;
    protected override string DisplayName => "Бомбы";

    private void OnEnable()
    {
        _cubePool.CubeReturned += SpawnBomb;
    }

    private void OnDisable()
    {
        _cubePool.CubeReturned -= SpawnBomb;
    }

    private void SpawnBomb(Vector3 position)
    {
        _bombPool.GetBomb(position);
    }
}