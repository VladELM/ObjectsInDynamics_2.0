using static UnityEngine.Random;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Queue<Bomb> _bombsPool;
    private int _poolSize;

    private void Awake()
    {
        _bombsPool = new Queue<Bomb>();
        _poolSize = _cubeSpawner.MaxPoolSize * 2;
        
        for (int i = 0; i < _poolSize; i++)
        {
            Bomb bomb = Instantiate(_bombPrefab);
            bomb.gameObject.SetActive(false);
            _bombsPool.Enqueue(bomb);
        }

        
        //_cubeSpawner.CubeWasGivenBack += GetFromPool;
    }

    protected override Bomb GetFromPool(Vector3 position)
    {
        Bomb bomb = base.GetFromPool(position);
        bomb.TimeCounted += GiveBackToPool;

        return bomb;
    }

    protected override void GiveBackToPool(Bomb bomb)
    {
        bomb.TimeCounted -= GiveBackToPool;
        base.GiveBackToPool(bomb);
    }
}
