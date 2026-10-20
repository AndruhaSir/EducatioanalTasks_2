using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField, Min(1)] private int _initialSize = 20;

    private readonly Queue<Cube> _availableCubes = new Queue<Cube>();

    private void Awake()
    {
        for (int index = 0; index < _initialSize; index++)
        {
            _availableCubes.Enqueue(CreateCube());
        }
    }

    public Cube GetCube(Vector3 position)
    {
        Cube cube = _availableCubes.Count > 0 ? _availableCubes.Dequeue() : CreateCube();

        cube.transform.position = position;
        cube.transform.rotation = Random.rotation;
        cube.gameObject.SetActive(true);
        cube.Initialize(this);

        return cube;
    }

    public void ReturnCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _availableCubes.Enqueue(cube);
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(_cubePrefab, transform);
        cube.gameObject.SetActive(false);
        return cube;
    }
}