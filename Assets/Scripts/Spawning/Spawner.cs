using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner<T> : MonoBehaviour where T: DestroyableObject
{
    [SerializeField] protected int _maxPoolSize;

    protected Queue<T> _pool;

    protected virtual T GetFromPool(Vector3 position)
    {
        T destroyableObject = _pool.Dequeue();
        destroyableObject.gameObject.SetActive(true);
        destroyableObject.Initialize(position);

        return destroyableObject;
    }

    protected virtual void GiveBackToPool(T destroyableObject)
    {
        destroyableObject.gameObject.SetActive(false);
        _pool.Enqueue(destroyableObject);
    }
}
