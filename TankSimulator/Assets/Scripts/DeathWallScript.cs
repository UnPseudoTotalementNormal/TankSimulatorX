using UnityEngine;
using CandyCoded.HapticFeedback;
using Cinemachine;

public class DeathWallScript : MonoBehaviour
{
    public static DeathWallScript Instance;
    [SerializeField] private CinemachineImpulseSource CinemachineImpulseSource;
    private Rigidbody2D _playerRigidbody;

    [SerializeField] private float _playerDistance = 20;
    [SerializeField] private float _maxTimeBeforeDeath = 3; public float MaxTimeBeforeDeath { get { return _maxTimeBeforeDeath; } }
    [SerializeField] private float _fallSpeedDeathThreshold = 15; public float FallSpeedDeathThreshold { get { return _fallSpeedDeathThreshold; } }
    private float _timeBeforeDeath = 3; public float TimeBeforeDeath { get { return _timeBeforeDeath; } }

    private new Transform transform;

    private void Awake()
    {
        Instance = this;
        transform = GetComponent<Transform>();
    }

    private void Start()
    {
        _playerRigidbody = PlayerController.Instance.GetComponent<Rigidbody2D>();
        UpdatePos();
    }

    private void Update()
    {
        if (GameManager.Instance.GameStarted)
        {
            if (-_playerRigidbody.velocity.y < _fallSpeedDeathThreshold)
            {
                _timeBeforeDeath = Mathf.Clamp(_timeBeforeDeath - Time.deltaTime, -4, MaxTimeBeforeDeath);
            }
            else
            {
                _timeBeforeDeath = Mathf.Clamp(_timeBeforeDeath + Time.deltaTime, -1, MaxTimeBeforeDeath);
            }

            UpdatePos();

            if (_timeBeforeDeath < 0 && !GameManager.Instance.GameEnded)
            {
                GameManager.Instance.EndGame();
                HapticFeedback.HeavyFeedback();
            }

            if (_timeBeforeDeath < 1 && !GameManager.Instance.GameEnded)
            {
                HapticFeedback.LightFeedback();
                
            }
        }
        CinemachineImpulseSource.GenerateImpulse();
    }

    private void UpdatePos()
    {
        transform.position = new Vector3(0, PlayerController.Instance.transform.position.y, -5) + Vector3.up * (_playerDistance * (_timeBeforeDeath / _maxTimeBeforeDeath));
    }
}
