using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _visuals;

    [SerializeField] private float _visualFloatHeight = 1;
    [SerializeField] private float _visualFloatSpeed = 1;

    [SerializeField] private GameObject _deathParticles;

    public UnityEvent DeathEvent;

    protected private virtual void Death()
    {
        DeathEvent?.Invoke();
        if (_deathParticles) Destroy(Instantiate(_deathParticles, _visuals.transform.position, Quaternion.identity), 4);
        Destroy(gameObject);
    }

    protected private virtual void Update()
    {
        Floating();
    }

    private void Floating()
    {
        _visuals.localPosition = new Vector3(_visuals.localPosition.x, Mathf.Sin(Time.time * _visualFloatSpeed) * _visualFloatHeight, _visuals.localPosition.z);
    }
}
