using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private MeshRenderer _render;
    private string _planeTag = "Plane";
    private Coroutine _coroutine;
    private bool _touched;

    public event Action<Cube> Destroing;

    private void Awake()
    {
        _render = GetComponent<MeshRenderer>();

        _touched = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == _planeTag)
        {
            if(_touched == false)
            {
                ChangeColor();
                StartTimerForDestroy();
                _touched = true;
            }
        }
    }

    private void OnEnable()
    {
        _touched = false;
        _render.material.color = Color.white;
    }

    private void ChangeColor()
    {
        _render.material.color = UnityEngine.Random.ColorHSV();
    }

    private void StartTimerForDestroy()
    {
        if(_coroutine != null)
            StopCoroutine( _coroutine);

        _coroutine = StartCoroutine(DoTimerForDestroy());
    }

    private IEnumerator DoTimerForDestroy()
    {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));

        Destroing?.Invoke(gameObject.GetComponent<Cube>());
    }
}
