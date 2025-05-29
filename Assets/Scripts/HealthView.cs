using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _bar;
    [SerializeField] private Slider _smoothBar;
    [SerializeField] private float _changeSpeed;

    private Coroutine _coroutine;

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
        StartChangeValue(valueForBar);
    }

    public void StartChangeValue(float value)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValue(value));
    }

    private IEnumerator ChangeValue(float value)
    {
        while (Mathf.Approximately(_smoothBar.value, value) == false)
        {
            _smoothBar.value = Mathf.MoveTowards(_smoothBar.value, value, _changeSpeed);
            yield return null;
        }
    }
}
