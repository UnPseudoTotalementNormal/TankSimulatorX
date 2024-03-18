using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private bool _gameStarted = true; public bool GameStarted { get { return _gameStarted; } }

    public float Coins = 0;
    private bool _gameEnded = false; public bool GameEnded { get { return _gameEnded; } }

    public UnityEvent GameEndedEvent;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
    }

    public void EndGame()
    {
        if (!_gameEnded)
        {
            _gameEnded = true;
            GameEndedEvent?.Invoke();
        }
    }
}
