using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner<T> : MonoBehaviour, ISpawnable where T: DestroyableObject
{
    [SerializeField] protected int _maxPoolSize;
    [SerializeField] private T _prefab;

    protected Queue<T> _pool;

    public event Action<int> ObjectsCreated;
    public event Action ObjectSpawned;
    public event Action<int> ObjectTaked;
    public event Action<int> ObjectGivenBack;

    protected void CreatePool()
    {
        _pool = new Queue<T>();

        for (int i = 0; i < _maxPoolSize; i++)
        {
            T spawnObject = Instantiate(_prefab);
            spawnObject.gameObject.SetActive(false);
            _pool.Enqueue(spawnObject);
        }

        ObjectsCreated?.Invoke(_pool.Count);
    }

    protected virtual T GetFromPool(Vector3 position)
    {
        T destroyableObject = _pool.Dequeue();
        destroyableObject.gameObject.SetActive(true);
        destroyableObject.Initialize(position);
        ObjectSpawned?.Invoke();
        ObjectTaked?.Invoke(_maxPoolSize - _pool.Count);

        return destroyableObject;
    }

    protected virtual void GiveBackToPool(T destroyableObject)
    {
        destroyableObject.gameObject.SetActive(false);
        _pool.Enqueue(destroyableObject);
        ObjectGivenBack?.Invoke(_pool.Count);
    }
}
