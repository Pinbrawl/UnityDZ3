using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxCount;

    private int _minCount = 0;
    private int _count;

    public event Action<int, int> Changed;

    private void Awake()
    {
        _count = _maxCount;
        Changed?.Invoke(_count, _maxCount);
    }

    public void TakeDamage(int count)
    {
        _count = Mathf.Clamp(_count - count, _minCount, _maxCount);
        Changed?.Invoke(_count, _maxCount);
    }

    public void Heal(int count)
    {
        _count = Mathf.Clamp(_count + count, _minCount, _maxCount);
        Changed?.Invoke(_count, _maxCount);
    }
}
