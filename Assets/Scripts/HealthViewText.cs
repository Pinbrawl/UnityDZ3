using TMPro;
using UnityEngine;

public class HealthViewText : HealthView
{
    [SerializeField] private TMP_Text _text;

    protected virtual void Print(int health, int maxHealth)
    {
        _text.text = health + "/" + maxHealth;
    }
}
