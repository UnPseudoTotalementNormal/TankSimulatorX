using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float _maxHeightDif = 3f;
    public float SmoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;

    [SerializeField] private float _baseLerpSpeed = 4;
    [SerializeField] private float _maxLerpSpeed = 10;
    private float _lerpSpeed = 4;


    void FixedUpdate()
    {
        if (_player != null)
        {
            Vector3 targetPosition = _player.transform.position + _offset - new Vector3(_player.transform.position.x, 0, 0);
            float heightDiff = Mathf.Abs(transform.position.y - targetPosition.y);
            if (heightDiff > _maxHeightDif)
            {
                _lerpSpeed = Mathf.Lerp(_lerpSpeed, _maxLerpSpeed, 4 * Time.deltaTime);
            }
            else
            {
                _lerpSpeed = Mathf.Lerp(_lerpSpeed, _baseLerpSpeed, 4 * Time.deltaTime);
                //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, SmoothTime);
            }
            transform.position = Vector3.Lerp(transform.position, targetPosition, _lerpSpeed * Time.fixedDeltaTime);
        }
        else
            Debug.LogError("You forget to put the player in serialize field in CameraFollowPlayer script");
    }

}
