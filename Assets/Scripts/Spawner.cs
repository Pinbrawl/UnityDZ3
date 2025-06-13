using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _object;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _maxObjects;
    [SerializeField] private int _interval;

    private List<bool> _occupiedPoints;
    private ObjectPool<Enemy> _pool;
    private int _objects;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => InstantiateObj(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => DestroyObj(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        _occupiedPoints = new List<bool>();

        for (int i = 0; i < _points.Count; i++)
            _occupiedPoints.Add(false);

        StartCoroutine(Spawn());
    }

    private Enemy InstantiateObj()
    {
        Enemy enemy = Instantiate(_object);
        enemy.Died += ActionOnRelease;

        return enemy;
    }

    private void ActionOnGet(Enemy obj)
    {
        obj.gameObject.SetActive(true);

        System.Random random = new System.Random();

        int randomNumber = random.Next(_points.Count);

        while (_occupiedPoints[randomNumber] == true)
            randomNumber = random.Next(_points.Count);

        obj.transform.position = _points[randomNumber].transform.position;
        _occupiedPoints[randomNumber] = true;

        _objects++;
    }

    private void ActionOnRelease(Enemy obj)
    {
        for(int i = 0; i < _points.Count; i++)
        {
            if(obj.transform.position == _points[i].transform.position)
            {
                _occupiedPoints[i] = false;
            }
        }

        obj.gameObject.SetActive(false);
        _objects--;
    }

    private void DestroyObj(Enemy obj)
    {
        obj.Died -= ActionOnRelease;
        Destroy(obj);
    }

    private IEnumerator Spawn()
    {
        var interval = new WaitForSecondsRealtime(_interval);

        while(enabled)
        {
            if(_objects < _maxObjects)
            {
                yield return interval;

                _pool.Get();
            }
            else
            {
                yield return null;
            }
        }
    }
}
