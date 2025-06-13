using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    public void Shoot()
    {
        Instantiate(_bullet, transform.position, transform.rotation);
    }
}
