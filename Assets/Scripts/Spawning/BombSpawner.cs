using static UnityEngine.Random;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Awake()
    {
        CreatePool();
        _cubeSpawner.CubeWasGivenBack += GetFromPool;
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
