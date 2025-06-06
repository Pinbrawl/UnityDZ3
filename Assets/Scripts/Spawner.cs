using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _obj;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    protected ObjectPool<T> _pool;

    private int _spawnedCount;
    private int _InstantiatedCount;

    public event Action<int> Spawned;
    public event Action<int> Instantiated;
    public event Action<int> ActiveCountChanged;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => InstantiateObj(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => DestroyObj(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        _spawnedCount = 0;
        _InstantiatedCount = 0;
    }

    protected virtual T InstantiateObj()
    {
        T obj = Instantiate(_obj);
        Instantiated?.Invoke(++_InstantiatedCount);

        return obj;
    }

    protected virtual void DestroyObj(T obj)
    {
        Destroy(obj);
    }

    private void ActionOnGet(T obj)
    {
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.gameObject.SetActive(true);

        ActiveCountChanged?.Invoke(_pool.CountActive);
    }

    protected virtual void ActionOnRelease(T obj)
    {
        obj.gameObject.SetActive(false);

        ActiveCountChanged?.Invoke(_pool.CountActive);
    }

    protected void Spawn(Vector3 position)
    {
        T obj = _pool.Get();
        obj.transform.position = position;

        Spawned?.Invoke(++_spawnedCount);
    }
}
