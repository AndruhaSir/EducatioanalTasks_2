using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private Transform _mainPlatform;
    [SerializeField, Min(0.05f)] private float _spawnInterval = 0.5f;
    [SerializeField, Min(0f)] private float _spawnAreaSize = 4f;
    [SerializeField] private GameObject _upperSpawnPont;

    private Coroutine _spawnCoroutine;

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnCubes());
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
        if(_upperSpawnPont == null || _mainPlatform == null || _cubePool == null)
        {
            Debug.Log("Не все компоненты подключены");
        }
    }

    private IEnumerator SpawnCubes()
    {
        while (true)
        {
            SpawnCube();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnCube()
    {
        Vector3 platformPosition = _mainPlatform.position;

        Vector3 spawnPosition = new Vector3(
            platformPosition.x + Random.Range(-_spawnAreaSize, _spawnAreaSize),
            platformPosition.y + _upperSpawnPont.transform.position.y,
            platformPosition.z + Random.Range(-_spawnAreaSize, _spawnAreaSize)
        );

        _cubePool.GetCube(spawnPosition);
    }
}