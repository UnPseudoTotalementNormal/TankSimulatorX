using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private bool _gameStarted = true; public bool GameStarted { get { return _gameStarted; } }

    public float Coins = 0;
    private bool _gameEnded = false; public bool GameEnded { get { return _gameEnded; } }

    public UnityEvent GameEndedEvent;
    public UnityEvent GameStartedEvent;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
    }

    public void StartGame()
    {
        if (!_gameStarted)
        {
            _gameStarted = true;
            GameStartedEvent?.Invoke();
        }
    }

    public void EndGame()
    {
        if (!_gameEnded)
        {
            Time.timeScale = 1;
            _gameEnded = true;
            GameEndedEvent?.Invoke();
            Invoke("ReloadGame", 3);
        }
    }

    public bool IsPlaying()
    {
        return _gameStarted && !_gameEnded;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
