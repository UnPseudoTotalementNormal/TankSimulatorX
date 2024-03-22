using UnityEngine;
using CandyCoded.HapticFeedback;

public class Booster : Enemy
{
    [SerializeField] private TriggerScript _triggerScript;

    [SerializeField] private float _pushPlayerForce = 10;

    private void Awake()
    {
        _triggerScript.TriggerEnterEvent.AddListener(TriggerEnter);
    }

    private void Start()
    {
        
    }

    private void TriggerEnter()
    {
        Transform colliderObject = _triggerScript.LastColliderEnter.transform.parent;
        if (colliderObject == PlayerController.Instance.transform)
        {
            colliderObject.GetComponent<Rigidbody2D>().AddForce(Vector3.down * _pushPlayerForce, ForceMode2D.Impulse);
            Death();
            SoundManager.Instance.PlayAtPath("SFX/SpeedBoost", 0.1f);
            HapticFeedback.MediumFeedback();
        }
    }
}
