using UnityEngine;

public class DeathWallScript : MonoBehaviour
{
    public static DeathWallScript Instance;

    private float _playerDistance = 20;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
        transform.position = new Vector3(0, PlayerController.Instance.transform.position.y, -5) + Vector3.up * (_playerDistance * (GameManager.Instance.TimeBeforeDeath / GameManager.Instance.MaxTimeBeforeDeath));
    }
}
