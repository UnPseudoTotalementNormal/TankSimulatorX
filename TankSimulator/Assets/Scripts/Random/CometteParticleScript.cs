using UnityEngine;

public class CometteParticleScript : MonoBehaviour
{
    [SerializeField] private float _minRateOverTime;
    [SerializeField] private float _maxRateOverTime;

    [SerializeField] private float _minSpeedRateOverTime;
    [SerializeField] private float _maxSpeedRateOverTime;

    private Rigidbody2D _playerRb;
    private ParticleSystem _particleSystem;
    private ParticleSystem.EmissionModule _particleEmission;
    private ParticleSystem.MainModule _particleMain;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleEmission = _particleSystem.emission;
        _particleMain = _particleSystem.main;
    }

    private void Start()
    {
        _playerRb = PlayerController.Instance.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float speed = Mathf.Abs(_playerRb.velocity.y);

        //float rateOverTime = Mathf.Lerp(_minRateOverTime, _maxRateOverTime, speed / _playerRb.velocity.magnitude);

        float rate = Mathf.Lerp(_minRateOverTime, _maxRateOverTime, Mathf.InverseLerp(_minSpeedRateOverTime, _maxSpeedRateOverTime, speed));

        _particleEmission.rateOverTime = rate;
    }
}
