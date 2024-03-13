using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rb;
    [SerializeField] private Transform _canonPivot;

    private Vector2 _lastJoystickValue;

    [SerializeField] private float _canonForce = 5;


    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InputManager.Instance.TouchPositionEvent.AddListener(UpdateCanonOrientation);
        InputManager.Instance.ReleaseClickEvent.AddListener(ShootCanon);
    }

    private void Update()
    {

    }

    private void UpdateCanonOrientation()
    {
        Vector2 jValue = InputManager.Instance.GetJoystickValue();
        _canonPivot.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(jValue.x, -jValue.y) * Mathf.Rad2Deg);
        _lastJoystickValue = jValue;
    }

    private void ShootCanon()
    {
        _rb.AddForce(-_lastJoystickValue.normalized * _canonForce, ForceMode2D.Impulse);
    }
}
