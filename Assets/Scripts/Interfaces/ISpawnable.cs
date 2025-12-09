using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    public event Action OnSpawn;

    public event Action<int> ObjectsCreated;
    public event Action ObjectSpawned;
    public event Action<int> ObjectTaked;
    public event Action<int> ObjectGivenBack;
}
