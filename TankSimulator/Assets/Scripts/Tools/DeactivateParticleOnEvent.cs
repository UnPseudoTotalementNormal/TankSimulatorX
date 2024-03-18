using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateParticleOnEvent : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void OnEvent()
    {
        ParticleSystem.EmissionModule emissionModule = _particleSystem.emission;
        emissionModule.rateOverTime = 0;
    }
}
