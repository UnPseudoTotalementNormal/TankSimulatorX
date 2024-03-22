using UnityEngine;

public class WindSFXScript : MonoBehaviour
{
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;

    [SerializeField] private float _minSpeedVolume;
    [SerializeField] private float _maxSpeedVolume;

    private Rigidbody2D _playerRb;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerRb = PlayerController.Instance.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_playerRb != null && _audioSource != null)
        {
            float speed = _playerRb.velocity.magnitude;
            float volume = Mathf.Lerp(_minVolume, _maxVolume, Mathf.InverseLerp(_minSpeedVolume, _maxSpeedVolume, speed));

            _audioSource.volume = Mathf.Lerp(_audioSource.volume, volume, 3 * Time.unscaledDeltaTime);
        }
    }
}
