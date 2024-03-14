using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rb;
    [SerializeField] private Transform _canonPivot;

    [SerializeField] private Transform _endCanon;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private GameObject _shootParticle;

    private Vector2 _lastJoystickValue;

    [SerializeField] private float _maxFallSpeed = 10;
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
        if (_rb.velocity.y < -_maxFallSpeed)
        {
            _rb.gravityScale = -0.15f;
        }
        else
        {
            _rb.gravityScale = 2;
        }

        if (InputManager.Instance.IsAiming())
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.25f, 4 * Time.timeScale);
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 4 * Time.timeScale);
        }
    }

    private void UpdateCanonOrientation()
    {
        Vector2 jValue = InputManager.Instance.GetJoystickValue();
        _canonPivot.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(jValue.x, -jValue.y) * Mathf.Rad2Deg);
        _lastJoystickValue = jValue;
    }

    private void ShootCanon()
    {
        float addedForce = 0;
        if (Vector3.Dot(_lastJoystickValue.normalized, _rb.velocity) >= _canonForce/2)
        {
            addedForce += _canonForce/2;
        }
        _rb.AddForce(-_lastJoystickValue.normalized * (_canonForce + addedForce), ForceMode2D.Impulse);

        if (_bullet)
        {

        }
        else Debug.LogWarning("No bullet on playercontroller");

        if (_shootParticle)
        {
            Destroy(Instantiate(_shootParticle, _endCanon.position, _canonPivot.rotation), 5);
        }
        else Debug.LogWarning("No shootParticle on playercontroller");
    }
}
