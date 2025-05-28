using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _bar;
    [SerializeField] private SmoothBar _smoothBar;

    private void OnEnable()
    {
        _health.Changed += Print;
    }

    private void OnDisable()
    {
        _health.Changed -= Print;
    }

    private void Print(int health, int maxHealth)
    {
        _text.text = health + "/" + maxHealth;

        float valueForBar = (float)health / maxHealth;
        _bar.value = valueForBar;
        _smoothBar.StartChangeValue(valueForBar);
    }
}
