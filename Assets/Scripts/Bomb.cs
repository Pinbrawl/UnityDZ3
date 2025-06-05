using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _smooth;

    private Coroutine _coroutine;
    private MeshRenderer _renderer;

    public event Action<Bomb> Exploding;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        _renderer.material.CopyPropertiesFromMaterial(_material);

        StartTimerForExplosion();
    }

    private void StartTimerForExplosion()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(DoTimerForExplosion());
    }

    private IEnumerator DoTimerForExplosion()
    {
        float time = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        var interval = new WaitForSecondsRealtime(time * _smooth);
        int count = (int)(1 / _smooth);

        for(int i = 0; i < count; i++)
        {
            yield return interval;
            _renderer.material.color -= new Color(0, 0, 0, _smooth);
        }

        Explode();
        Exploding?.Invoke(gameObject.GetComponent<Bomb>());
    }

    private void Explode()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach(Collider collider in _colliders)
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}
