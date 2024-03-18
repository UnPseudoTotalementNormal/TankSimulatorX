using UnityEngine;
using UnityEngine.Events;

public class TriggerScript : MonoBehaviour
{
    public UnityEvent TriggerEnterEvent;
    public UnityEvent TriggerExitEvent;
    public UnityEvent TriggerStayEvent;

    private Collider2D _lastColliderEnter; public Collider2D LastColliderEnter {  get { return _lastColliderEnter; } }
    private Collider2D _lastColliderExit; public Collider2D LastColliderExit {  get { return _lastColliderExit; } }
    private Collider2D _lastColliderStay; public Collider2D LastColliderStay { get { return _lastColliderStay; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _lastColliderEnter = collision;
        TriggerEnterEvent?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _lastColliderExit = collision;
        TriggerExitEvent?.Invoke();    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _lastColliderStay = collision;
        TriggerStayEvent?.Invoke();
    }
}
