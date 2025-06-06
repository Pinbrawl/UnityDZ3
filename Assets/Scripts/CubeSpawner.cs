using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _interval;

    public event Action<Vector3> Releasing;

    private void Awake()
    {
        StartCoroutine(SpawnCubes());
    }

    protected override Cube InstantiateObj()
    {
        Cube cube = base.InstantiateObj();
        cube.Destroing += _pool.Release;

        return cube;
    }

    protected override void DestroyObj(Cube cube)
    {
        cube.Destroing -= _pool.Release;

        base.DestroyObj(cube);
    }

    protected override void ActionOnRelease(Cube cube)
    {
        Releasing?.Invoke(cube.transform.position);

        base.ActionOnRelease(cube);
    }

    private Vector3 GetRandomPosition()
    {
        return transform.position + new Vector3(UnityEngine.Random.Range(-transform.localScale.x, transform.localScale.x), UnityEngine.Random.Range(-transform.localScale.y, transform.localScale.y));
    }

    private IEnumerator SpawnCubes()
    {
        var interval = new WaitForSecondsRealtime(_interval);

        while(enabled)
        {
            Spawn(GetRandomPosition());

            yield return interval;
        }
    }
}
