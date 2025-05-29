using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Damager : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private int _count;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(DealDamage);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(DealDamage);
    }

    private void DealDamage()
    {
        _health.TakeDamage(_count);
    }
}
