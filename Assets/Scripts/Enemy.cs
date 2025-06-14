using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Health _health;
    [SerializeField] private float _shootInterval;

    public event Action<Enemy> Died;

    private void Awake()
    {
        StartCoroutine(Shoot());
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    private IEnumerator Shoot()
    {
        var interval = new WaitForSecondsRealtime(_shootInterval);

        while(enabled)
        {
            yield return interval;

            _gun.Shoot();
        }
    }

    private void Die()
    {
        Died?.Invoke(this);
    }
}
