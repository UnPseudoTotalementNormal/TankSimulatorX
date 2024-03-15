using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _timeBeforeDestroy = 5;
    [SerializeField] private GameObject _deathParticle;
    [SerializeField] private GameObject _lightExplosion;

    private void Awake()
    {
        Destroy(gameObject, _timeBeforeDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform collParent = collision.transform.parent;
        if (collParent && collParent.CompareTag("Breakable"))
        {
            if (collParent.TryGetComponent<BreakableBlocScript>(out BreakableBlocScript breakableBlocScript))
                breakableBlocScript.Death();

            Destroy(collParent.gameObject);
            Death();
        }
    }

    public void Death()
    {
        if (_deathParticle) Destroy(Instantiate(_deathParticle, transform.position, Quaternion.identity), 5);

        if (_lightExplosion) Instantiate(_lightExplosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
