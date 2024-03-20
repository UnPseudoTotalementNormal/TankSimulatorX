using TMPro;
using UnityEngine;

public class PlayerWorldCanvas : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private TextMeshProUGUI _fallSpeedText;

    [SerializeField] private GameObject _dangerIcon;
    [SerializeField] private float _dangerDelay = 0;
    private float _dangerTime = 0;

    private void Start()
    {
        _playerController = PlayerController.Instance;
    }

    private void Update()
    {
        FallSpeedUpdate();

        _dangerTime += Time.deltaTime;
        if (DeathWallScript.Instance.TimeBeforeDeath < 1.5f && DeathWallScript.Instance.TimeBeforeDeath > 0)
        {
            if (_dangerTime >= _dangerDelay)
            {
                _dangerIcon.SetActive(!_dangerIcon.activeSelf);
                _dangerTime = 0;
            }
        }
        else
        {
            _dangerIcon.SetActive(false);
        }
    }

    private void FallSpeedUpdate()
    {
        //text
        Transform textTransform = _fallSpeedText.transform;
        float maxSidePos = (Camera.main.orthographicSize / 2) / 1.5f;
        textTransform.localPosition = new Vector3(0, textTransform.localPosition.y, textTransform.localPosition.z);
        textTransform.position = new Vector3(Mathf.Clamp(textTransform.position.x, -maxSidePos, maxSidePos), textTransform.position.y, textTransform.position.z);
        _fallSpeedText.text = ((int)-_playerRb.velocity.y).ToString() + "<size=-50>km/h";

        //color
        if (_playerController.IsUnderSpeedThreshold())
            _fallSpeedText.color = Color.Lerp(_fallSpeedText.color, Color.red, 4 * Time.deltaTime);
        else
            _fallSpeedText.color = Color.Lerp(_fallSpeedText.color, Color.black, 4 * Time.deltaTime);
    }
}
