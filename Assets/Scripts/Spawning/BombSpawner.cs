using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Awake()
    {
        CreatePool();
        _cubeSpawner.CubeWasGivenBack += GetFromPool;
    }
}
