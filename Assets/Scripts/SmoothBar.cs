using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothBar : MonoBehaviour
{
    [SerializeField] private float _changeSpeed;

    private Slider _slider;
    private Coroutine _coroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void StartChangeValue(float value)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValue(value));
    }

    private IEnumerator ChangeValue(float value)
    {
        while(Mathf.Approximately(_slider.value, value) == false)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, value, _changeSpeed);
            yield return null;
        }
    }
}
