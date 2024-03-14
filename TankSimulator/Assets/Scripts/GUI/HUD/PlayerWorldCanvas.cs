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
        _fallSpeedText.text = ((int)-_playerRb.velocity.y).ToString() + "<size=-50>km/h";
    }
}
