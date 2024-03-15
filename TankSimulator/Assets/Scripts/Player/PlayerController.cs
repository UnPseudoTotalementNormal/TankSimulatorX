using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Transform _transform;
    private Rigidbody2D _rb;
    [SerializeField] private Transform _canonPivot;

    [SerializeField] private Transform _endCanon;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private GameObject _shootParticle;
    [SerializeField] private GameObject _shootLightExplosion;

    private Vector2 _lastJoystickValue;

    [SerializeField] private float _maxFallSpeed = 10;
    [SerializeField] private float _maxRecoilFallSpeed = 20;
    [SerializeField] private float _fallSpeedDeathThreshold = 15;

    [SerializeField] private float _canonShootRecoil = 5;
    [SerializeField] private float _canonShootForce = 5;


    private void Awake()
    {
        Instance = this;
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
        if (jValue != Vector2.zero)
        {
            _canonPivot.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(jValue.x, -jValue.y) * Mathf.Rad2Deg);
            _lastJoystickValue = jValue;
        }
        
    }

    private void ShootCanon()
    {
        HapticFeedback.LightFeedback();

        Vector3 shootDirection = -_lastJoystickValue.normalized;

        float addedForce = 0;
        if (Vector3.Dot(-shootDirection, _rb.velocity) >= _canonShootRecoil/3)
        {
            addedForce += _canonShootRecoil/2;
        }

        float downVel = Vector3.Dot(Vector3.down, _rb.velocity);
        float nextDownVel = downVel + (Vector3.Dot(shootDirection * _canonShootRecoil, Vector3.down));
        if (nextDownVel > _maxRecoilFallSpeed)
        {
            shootDirection.y = 0;
            _rb.AddForce(Vector3.down * Mathf.Clamp(_maxRecoilFallSpeed - downVel, 0, _maxRecoilFallSpeed), ForceMode2D.Impulse);
        }

        _rb.AddForce(shootDirection * (_canonShootRecoil + addedForce), ForceMode2D.Impulse);

        if (_bullet)
        {
            Rigidbody2D bRb = Instantiate(_bullet, _endCanon.position, _endCanon.rotation).GetComponent<Rigidbody2D>();
            bRb.AddForce(_canonShootForce * _lastJoystickValue.normalized, ForceMode2D.Impulse);
        }
        else Debug.LogWarning("No bullet on playercontroller");

        if (_shootParticle) Destroy(Instantiate(_shootParticle, _endCanon.position, Quaternion.Inverse(_canonPivot.rotation)), 5);
        else Debug.LogWarning("No shootParticle on playercontroller");

        if (_shootLightExplosion) Instantiate(_shootLightExplosion, _endCanon.position, Quaternion.identity);
        else Debug.LogWarning("No shootLightExplosion on playercontroller");
    }

    public bool IsUnderSpeedThreshold()
    {
        return -_rb.velocity.y < _fallSpeedDeathThreshold;
    }
}
