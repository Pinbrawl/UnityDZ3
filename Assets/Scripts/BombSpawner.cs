using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.Releasing += Spawn;
    }

    private void OnDisable()
    {
        _cubeSpawner.Releasing -= Spawn;
    }

    protected override Bomb InstantiateObj()
    {
        Bomb bomb = base.InstantiateObj();
        bomb.Exploding += _pool.Release;

        return bomb;
    }

    protected override void DestroyObj(Bomb obj)
    {
        obj.Exploding -= _pool.Release;

        base.DestroyObj(obj);
    }
}
