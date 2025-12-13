using System;

public interface ISpawnable
{
    public event Action<int> ObjectsCreated;
    public event Action ObjectSpawned;
    public event Action<int> ObjectTaked;
    public event Action<int> ObjectGivenBack;
}
