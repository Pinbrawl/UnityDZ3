using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private void Awake()
    {
        StartCoroutine(Move());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator Move()
    {
        while(enabled)
        {
            if(transform.rotation.y == 0)
                transform.position += Vector3.right * Time.deltaTime * _speed;
            else
                transform.position += Vector3.left * Time.deltaTime * _speed;

            yield return null;
        }
    }
}
