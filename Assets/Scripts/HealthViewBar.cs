using UnityEngine;
using UnityEngine.UI;

public class HealthViewBar : HealthView
{
    [SerializeField] private Slider _bar;

    protected override void Print(int health, int maxHealth)
    {
        _bar.value = (float)health / maxHealth;
    }
}
