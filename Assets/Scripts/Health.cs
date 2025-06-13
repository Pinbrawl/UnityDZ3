using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxCount;

    private int _count;

    public event Action Died;

    private void Awake()
    {
        _count = _maxCount;
    }

    public void TakeDamage(int damage)
    {
        _count = Mathf.Clamp(_count - damage, 0, _maxCount);

        if (_count == 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
