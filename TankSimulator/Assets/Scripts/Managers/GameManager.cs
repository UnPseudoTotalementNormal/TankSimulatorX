using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float Coins = 0;
    [SerializeField] private float _maxTimeBeforeDeath = 3; public float MaxTimeBeforeDeath { get { return _maxTimeBeforeDeath; } }
    private float _timeBeforeDeath = 3; public float TimeBeforeDeath {  get { return _timeBeforeDeath; } }

    private bool _gameEnded = false; public bool GameEnded { get { return _gameEnded; } }

    public UnityEvent GameEndedEvent;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (-PlayerController.Instance.GetComponent<Rigidbody2D>().velocity.y < PlayerController.Instance.FallSpeedDeathThreshold)
        {
            _timeBeforeDeath = Mathf.Clamp(_timeBeforeDeath - Time.deltaTime, -4, MaxTimeBeforeDeath);
        }
        else
        {
            _timeBeforeDeath = Mathf.Clamp(_timeBeforeDeath + Time.deltaTime, -1, MaxTimeBeforeDeath);
        }
        if (_timeBeforeDeath < 0 && !_gameEnded) 
        { 
            _gameEnded = true;
            GameEndedEvent?.Invoke();
        }
    }
}
