using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField, Min(1)] private int _initialSize = 20;

    private readonly Queue<Enemy> _availableEnemies = new Queue<Enemy>();

    private void Awake()
    {
        for (int index = 0; index < _initialSize; index++)
        {
            _availableEnemies.Enqueue(CreateEnemy());
        }
    }

    public Enemy GetEnemy()
    {
        Enemy enemy = _availableEnemies.Count > 0 ? _availableEnemies.Dequeue() : CreateEnemy();

        enemy.gameObject.SetActive(true);

        return enemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        if (enemy.gameObject.activeSelf == false)
        {
            return;
        }

        enemy.ResetState();
        enemy.gameObject.SetActive(false);
        _availableEnemies.Enqueue(enemy);
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab, transform);
        enemy.gameObject.SetActive(false);
        return enemy;
    }
}