using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -10f);
    public float SmoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;

    void FixedUpdate()
    {
        if (_player != null)
        {
            Vector3 targetPosition = _player.transform.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, SmoothTime);
        }
        else
            Debug.LogError("You forget to put the player in serialize field in CameraFollowPlayer script");
    }
}
