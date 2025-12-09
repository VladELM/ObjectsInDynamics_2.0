using static UnityEngine.Random;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private int _minCoordinate;
    [SerializeField] private int _maxCoordinate;
    [SerializeField] private int _minHeight;
    [SerializeField] private int _maxHeight;
    [SerializeField] private int _delay;

    private WaitForSeconds _interval;

    public event Func<Vector3, Bomb> CubeWasGivenBack;

    private void Awake()
    {
        CreatePool();
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