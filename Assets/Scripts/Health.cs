using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Button _takeDamageButton;
    [SerializeField] private Button _healButton;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    [SerializeField] private int _heal;

    private int _minHealth = 0;
    private int _health;

    public event Action<int, int> Changed;

    private void Awake()
    {
        _health = _maxHealth;
        Changed?.Invoke(_health, _maxHealth);
    }

    private void OnEnable()
    {
        _takeDamageButton.onClick.AddListener(TakeDamage);
        _healButton.onClick.AddListener(Heal);
    }

    private void OnDisable()
    {
        _takeDamageButton.onClick.RemoveListener(TakeDamage);
        _healButton.onClick.RemoveListener(Heal);
    }

    private void TakeDamage()
    {
        _health = Mathf.Clamp(_health - _damage, _minHealth, _maxHealth);
        Changed?.Invoke(_health, _maxHealth);
    }

    private void Heal()
    {
        _health = Mathf.Clamp(_health + _heal, _minHealth, _maxHealth);
        Changed?.Invoke(_health, _maxHealth);
    }
}
