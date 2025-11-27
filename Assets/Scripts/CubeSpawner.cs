using static UnityEngine.Random;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _maxPoolSize = 20;
    [SerializeField] private int _minCoordinate;
    [SerializeField] private int _maxCoordinate;
    [SerializeField] private int _minHeight;
    [SerializeField] private int _maxHeight;
    [SerializeField] private int _delay;

    //private Queue<Cube> _cubesPool;
    private WaitForSeconds _interval;

    public int MaxPoolSize => _maxPoolSize;

    public event Action<Vector3> CubeWasGivenBack;

    private void Awake()
    {
        _pool = new Queue<Cube>();

        for (int i = 0; i < _maxPoolSize; i++)
        {
            Cube cube = Instantiate(_cubePrefab);
            cube.gameObject.SetActive(false);
            _pool.Enqueue(cube);
        }

        _interval = new WaitForSeconds(_delay);
        StartCoroutine(Spawning());
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Range(_minCoordinate, _maxCoordinate + 1),
                            Range(_minHeight, _maxHeight + 1),
                            Range(_minCoordinate, _maxCoordinate + 1));
    }

    protected override Cube GetFromPool(Vector3 position)
    {
        Cube cube = base.GetFromPool(position);
        cube.TimeCounted += GiveBackToPool;
        
        return cube;
    }

    protected override void GiveBackToPool(Cube cube)
    {
        cube.TimeCounted -= GiveBackToPool;
        base.GiveBackToPool(cube);
        CubeWasGivenBack?.Invoke(cube.transform.position);
    }

    private IEnumerator Spawning()
    {
        while (enabled)
        {
            yield return _interval;

            if (_pool.Count > 0)
                GetFromPool(GetRandomPosition());
        }
    }
}