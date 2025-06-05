using System;
using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Bomb> _pool;

    public event Action Spawned;
    public event Action Instantiated;
    public event Action<int> ActiveCountChanged;

    private void Awake()
    {
        _pool = new ObjectPool<Bomb>(
            createFunc: () => InstantiateBomb(),
            actionOnGet: (bomb) => ActionOnGet(bomb),
            actionOnRelease: (bomb) => ActionOnRelease(bomb),
            actionOnDestroy: (bomb) => DestroyBomb(bomb),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void OnEnable()
    {
        _cubeSpawner.Releasing += Spawn;
    }

    private void OnDisable()
    {
        _cubeSpawner.Releasing -= Spawn;
    }

    private Bomb InstantiateBomb()
    {
        Bomb bomb = Instantiate(_bomb);
        bomb.Exploding += _pool.Release;
        Instantiated?.Invoke();

        return bomb;
    }

    private void DestroyBomb(Bomb bomb)
    {
        _bomb.Exploding -= _pool.Release;
        Destroy(bomb);
    }

    private void ActionOnGet(Bomb bomb)
    {
        bomb.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bomb.gameObject.SetActive(true);

        ActiveCountChanged?.Invoke(_pool.CountActive);
    }

    private void ActionOnRelease(Bomb bomb)
    {
        bomb.gameObject.SetActive(false);

        ActiveCountChanged?.Invoke(_pool.CountActive);
    }

    private void Spawn(Vector3 position)
    {
        Bomb bomb = _pool.Get();
        bomb.transform.position = position;

        Spawned?.Invoke();
    }
}
