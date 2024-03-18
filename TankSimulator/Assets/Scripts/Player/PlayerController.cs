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
    [SerializeField] private TriggerScript _downTrigger;

    [SerializeField] private Transform _endCanon;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private GameObject _shootParticle;
    [SerializeField] private GameObject _shootLightExplosion;
    [SerializeField] private GameObject _particleLightExplosion;

    private Vector2 _lastJoystickValue;

    [SerializeField] private float _maxFallSpeed = 10;
    [SerializeField] private float _maxRecoilFallSpeed = 20;
    [SerializeField] private float _fallSpeedDeathThreshold = 15; public float FallSpeedDeathThreshold { get {  return _fallSpeedDeathThreshold; } }

    [SerializeField] private float _fallSpeedBottomDestroyThreshold = 15;
    [SerializeField] private float _fallSpeedBottomDestroyPush = 5;

    [SerializeField] private float _canonShootRecoil = 5;
    [SerializeField] private float _canonShootForce = 5;


    private void Awake()
    {
        Instance = this;
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
        //_downTrigger.TriggerEnterEvent.AddListener(DownTouched);
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

    private void FixedUpdate()
    {
        CheckBottomDestroy();
    }

    private void CheckBottomDestroy() //if you're going fast you can destroy blocks
    {
        foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, transform.localScale.x / 2f, _rb.velocity.normalized, _rb.velocity.magnitude * Time.fixedDeltaTime * 1.75f, (1 << LayerMask.NameToLayer("Breakable"))))
        {
            if (_rb.velocity.y < -_fallSpeedBottomDestroyThreshold)
            {
                if (hit.collider.transform.parent.TryGetComponent<BreakableBlocScript>(out BreakableBlocScript breakableBlocScript))
                {
                    _rb.AddForce(-_rb.velocity.normalized * _fallSpeedBottomDestroyPush, ForceMode2D.Impulse);
                    breakableBlocScript.Death(true);
                    HapticFeedback.MediumFeedback();
                }
            }
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
        else if (nextDownVel < -_maxRecoilFallSpeed)
        {
            shootDirection.y = 0;
            _rb.AddForce(Vector3.up * Mathf.Clamp(_maxRecoilFallSpeed + downVel, 0, _maxRecoilFallSpeed), ForceMode2D.Impulse);
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

    private void DownTouched() //not used
    {
        Collider2D lastColl = _downTrigger.LastColliderEnter;
        if (lastColl.gameObject.CompareTag("Breakable"))
        {
            Destroy(lastColl.transform.parent.gameObject);
            lastColl.gameObject.SetActive(false);
            print(_rb.velocity.y);
        }
    }

    public bool IsUnderSpeedThreshold()
    {
        return -_rb.velocity.y < _fallSpeedDeathThreshold;
    }

    private void OnParticleCollision(GameObject other)
    {
        GameManager.Instance.Coins += 1;

        if (_particleLightExplosion) Instantiate(_particleLightExplosion, transform.position, Quaternion.identity);
    }
}
