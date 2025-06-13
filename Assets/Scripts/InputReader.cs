using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private KeyCode _jumpKey = KeyCode.Space;
    private KeyCode _shootKey = KeyCode.Mouse0;

    public event Action JumpKeyDown;
    public event Action ShootKeyDown;

    private void Update()
    {
        if(Input.GetKeyDown(_jumpKey))
            JumpKeyDown?.Invoke();

        if (Input.GetKeyDown(_shootKey))
            ShootKeyDown?.Invoke();
    }
}
