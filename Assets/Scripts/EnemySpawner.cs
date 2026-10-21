using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private EnemyPool _enemyPool;

    [Header("Spawn")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField, Min(0.05f)] private float _spawnInterval = 2f;

    private Coroutine _spawnCoroutine;

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnEnemies());
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

    private IEnumerator SpawnEnemies()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            SpawnEnemy();
            yield return wait;
        }
    }

    private void SpawnEnemy()
    {
        Transform point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Vector3 direction = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * Vector3.forward;

        Enemy enemy = _enemyPool.GetEnemy();

        enemy.transform.position = point.position;
        enemy.transform.rotation = Quaternion.LookRotation(direction);
        enemy.Initialize(direction);

        enemy.LifetimeOver += ReturnEnemy;
    }

    private void ReturnEnemy(Enemy enemy)
    {
        enemy.LifetimeOver -= ReturnEnemy;
        _enemyPool.ReturnEnemy(enemy);
    }
}