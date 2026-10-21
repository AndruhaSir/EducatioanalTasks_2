using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private EnemyPool _enemyPool;

    [Header("Spawn")]
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField, Min(0.05f)] private float _spawnInterval = 2f;

    private Coroutine _spawnCoroutine;

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(StartSpawn());
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private void OnValidate()
    {
        if (_enemyPool == null || _spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogWarning("Не все компоненты назначены", this);
        }
    }

    private IEnumerator StartSpawn()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        SpawnPoint spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        Enemy enemy = _enemyPool.Get();

        enemy.transform.position = spawnPoint.transform.position;
        enemy.Initialize(spawnPoint.TargetTransform);

        enemy.ReturnRequested += Return;
    }

    private void Return(Enemy enemy)
    {
        enemy.ReturnRequested -= Return;
        _enemyPool.Return(enemy);
    }
}