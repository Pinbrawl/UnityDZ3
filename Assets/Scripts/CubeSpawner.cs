using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _interval;

    private ObjectPool<Cube> _pool;

    public event Action<Vector3> Releasing;
    public event Action Spawned;
    public event Action Instantiated;
    public event Action<int> ActiveCountChanged;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => InstantiateCube(),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) => DestroyCube(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        StartCoroutine(Spawn());
    }

    private Cube InstantiateCube()
    {
        Cube cube = Instantiate(_cube);
        cube.Destroing += _pool.Release;
        Instantiated?.Invoke();

        return cube;
    }

    private void DestroyCube(Cube cube)
    {
        _cube.Destroing -= _pool.Release;
        Destroy(cube);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GetRandomPosition();
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.gameObject.SetActive(true);

        ActiveCountChanged?.Invoke(_pool.CountActive);
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);

        Releasing?.Invoke(cube.transform.position);
        ActiveCountChanged?.Invoke(_pool.CountActive);
    }

    private Vector3 GetRandomPosition()
    {
        return transform.position + new Vector3(UnityEngine.Random.Range(-transform.localScale.x, transform.localScale.x), UnityEngine.Random.Range(-transform.localScale.y, transform.localScale.y));
    }

    private IEnumerator Spawn()
    {
        var interval = new WaitForSecondsRealtime(_interval);

        while(enabled)
        {
            _pool.Get();
            Spawned?.Invoke();

            yield return interval;
        }
    }
}
