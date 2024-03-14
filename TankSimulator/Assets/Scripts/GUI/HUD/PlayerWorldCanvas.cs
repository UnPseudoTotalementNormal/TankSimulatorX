using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWorldCanvas : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private TextMeshProUGUI _fallSpeedText;

    private void Update()
    {
        FallSpeedUpdate();
    }

    private void FallSpeedUpdate()
    {
        Transform textTransform = _fallSpeedText.transform;
        float maxSidePos = (Camera.main.orthographicSize / 2) / 1.5f;
        textTransform.localPosition = new Vector3(0, textTransform.localPosition.y, textTransform.localPosition.z);
        textTransform.position = new Vector3(Mathf.Clamp(textTransform.position.x, -maxSidePos, maxSidePos), textTransform.position.y, textTransform.position.z);
        _fallSpeedText.text = ((int)-_playerRb.velocity.y).ToString() + "<size=-50>km/h";
    }
}
