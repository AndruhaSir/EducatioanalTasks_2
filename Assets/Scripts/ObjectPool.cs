using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _container;
    private readonly Queue<T> _available = new Queue<T>();

    public int CreatedCount { get; private set; }
    public int SpawnedCount { get; private set; }
    public int ActiveCount { get; private set; }

    public ObjectPool(T prefab, Transform container, int initialSize)
    {
        _prefab = prefab;
        _container = container;

        for (int i = 0; i < initialSize; i++)
            _available.Enqueue(Create());
    }

    public T Get(Vector3 position, Quaternion rotation, Action<T> onGet)
    {
        T item = _available.Count > 0 ? _available.Dequeue() : Create();
        item.transform.SetPositionAndRotation(position, rotation);
        item.gameObject.SetActive(true);
        SpawnedCount++;
        ActiveCount++;
        onGet?.Invoke(item);
        return item;
    }

    public void Return(T item, Action<T> onReturn)
    {
        if (item == null || !item.gameObject.activeSelf)
            return;

        item.gameObject.SetActive(false);
        _available.Enqueue(item);
        ActiveCount--;
        onReturn?.Invoke(item);
    }

    private T Create()
    {
        T item = UnityEngine.Object.Instantiate(_prefab, _container);
        CreatedCount++;
        item.gameObject.SetActive(false);
        return item;
    }
}
