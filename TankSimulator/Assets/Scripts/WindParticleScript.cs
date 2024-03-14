using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticleScript : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private ParticleSystem.MainModule _particleMain;
    private ParticleSystem.EmissionModule _particleEmission;
    [SerializeField] private Rigidbody2D _playerRB;

    [SerializeField] private float _particleAmountMult = 2.5f;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleMain = _particleSystem.main;
        _particleEmission = _particleSystem.emission;
    }

    private void Update()
    {
        if (Mathf.Abs(_playerRB.velocity.y) > 3)
        {
            _particleMain.startColor = Color.white;
        }
        else
        {
            _particleMain.startColor = new Color(0, 0, 0, 0);
        }

        _particleEmission.rateOverTime = Mathf.Abs(_playerRB.velocity.y) * _particleAmountMult;
    }
}
