using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlocScript : MonoBehaviour
{
    [SerializeField] private GameObject _blocBreakParticle;

    public void Death(bool screenshake = false)
    {
        if (_blocBreakParticle) Destroy(Instantiate(_blocBreakParticle, transform.position, Quaternion.identity), 5);
        if (screenshake)
        {
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }

        Destroy(gameObject);
    }
}
