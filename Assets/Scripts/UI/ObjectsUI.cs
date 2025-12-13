using UnityEngine;

public class ObjectsUI<T> : MonoBehaviour where T : ISpawnable
{
    [SerializeField] private T _spawner;
    [SerializeField] private NumberViewer _allSpawnedObjectsViewer;
    [SerializeField] private NumberViewer _createdObjectsViewer;
    [SerializeField] private NumberViewer _activeObjectsViewer;

    private void OnEnable()
    {
        _spawner.ObjectSpawned += _allSpawnedObjectsViewer.AssigneAllSpawnedObjectsAmount;
        _spawner.ObjectsCreated += _createdObjectsViewer.AssigneCreatedObjectsAmount;
        _spawner.ObjectTaked += _activeObjectsViewer.AssigneActiveObjectsAmount;
        _spawner.ObjectGivenBack += _activeObjectsViewer.AssigneActiveObjectsAmount;
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned -= _allSpawnedObjectsViewer.AssigneAllSpawnedObjectsAmount;
        _spawner.ObjectsCreated -= _createdObjectsViewer.AssigneCreatedObjectsAmount;
        _spawner.ObjectTaked -= _activeObjectsViewer.AssigneActiveObjectsAmount;
        _spawner.ObjectGivenBack -= _activeObjectsViewer.AssigneActiveObjectsAmount;
    }
}
