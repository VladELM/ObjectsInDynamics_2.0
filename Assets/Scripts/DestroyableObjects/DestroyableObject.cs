using System;
using UnityEngine;

public abstract class DestroyableObject : MonoBehaviour
{
    public event Action<DestroyableObject> TimeCounted;

    public abstract void Initialize(Vector3 position);

    protected void CallEvent()
    {
        TimeCounted?.Invoke(this);
    }
}
