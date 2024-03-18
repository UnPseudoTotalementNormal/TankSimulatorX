using UnityEngine;

public class DeathWallScript : MonoBehaviour
{
    public static DeathWallScript Instance;
    private Rigidbody2D _playerRigidbody;

    [SerializeField] private float _playerDistance = 20;
    [SerializeField] private float _maxTimeBeforeDeath = 3; public float MaxTimeBeforeDeath { get { return _maxTimeBeforeDeath; } }
    private float _timeBeforeDeath = 3; public float TimeBeforeDeath { get { return _timeBeforeDeath; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _playerRigidbody = PlayerController.Instance.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (-_playerRigidbody.velocity.y < PlayerController.Instance.FallSpeedDeathThreshold)
        {
            _timeBeforeDeath = Mathf.Clamp(_timeBeforeDeath - Time.deltaTime, -4, MaxTimeBeforeDeath);
        }
        else
        {
            _timeBeforeDeath = Mathf.Clamp(_timeBeforeDeath + Time.deltaTime, -1, MaxTimeBeforeDeath);
        }

        transform.position = new Vector3(0, PlayerController.Instance.transform.position.y, -5) + Vector3.up * (_playerDistance * (_timeBeforeDeath / _maxTimeBeforeDeath));

        if (_timeBeforeDeath < 0 && !GameManager.Instance.GameEnded)
        {
            GameManager.Instance.EndGame();
        }
    }
}
