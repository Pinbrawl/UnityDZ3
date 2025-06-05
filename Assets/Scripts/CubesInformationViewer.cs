using UnityEngine;

public class CubesInformationViewer : InformationViewer
{
    [SerializeField] private CubeSpawner _spawner;

    private void OnEnable()
    {
        _spawner.Spawned += PrintSpawned;
        _spawner.Instantiated += PrintInstantiated;
        _spawner.ActiveCountChanged += PrintActive;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= PrintSpawned;
        _spawner.Instantiated -= PrintInstantiated;
        _spawner.ActiveCountChanged -= PrintActive;
    }
}
