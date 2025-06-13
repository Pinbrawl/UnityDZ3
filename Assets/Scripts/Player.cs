using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private Gun _gun;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxAngle;
    [SerializeField] private int _maxVelocity;
    [SerializeField] private int _minVelocity;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _input.JumpKeyDown += Jump;
        _input.ShootKeyDown += Shoot;
    }

    private void OnDisable()
    {
        _input.JumpKeyDown -= Jump;
        _input.ShootKeyDown -= Shoot;
    }

    private void Update()
    {
        Rotate();
    }

    private void Jump()
    {
        _rigidbody.velocity = Vector2.up * _jumpForce;
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Clamp(_rigidbody.velocity.y, _minVelocity, _maxVelocity) / (_maxVelocity - _minVelocity) * 2 * _maxAngle);
    }

    private void Shoot()
    {
        _gun.Shoot();
    }
}
