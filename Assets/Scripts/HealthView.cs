using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Changed += Print;
    }

    private void OnDisable()
    {
        _health.Changed -= Print;
    }

    protected virtual void Print(int health, int maxHealth) { }
}
