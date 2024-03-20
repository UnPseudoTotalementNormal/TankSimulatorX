using TMPro;
using UnityEngine;

public class PBLineScript : MonoBehaviour
{
    [SerializeField] private TriggerScript _trigger;

    [SerializeField] private GameObject _particlesEnter;
    [SerializeField] private GameObject _lightExplosionEnter;
    [SerializeField] private GameObject _collectibleParticlesEnter;

    [SerializeField] private TextMeshProUGUI _textPB;

    private void Start()
    {
        _textPB.text = "Personnal best: " + (-transform.position.y).ToString() + "m";
    }

    public void OnPlayerEnter()
    {
        if (_collectibleParticlesEnter)
        {
            Destroy(Instantiate(_collectibleParticlesEnter, _trigger.LastColliderEnter.transform.position, Quaternion.identity), 5);
        }

        if (_particlesEnter)
        {
            Destroy(Instantiate(_particlesEnter, _trigger.LastColliderEnter.transform.position, Quaternion.identity), 5);
        }

        if (_lightExplosionEnter)
        {
            Instantiate(_lightExplosionEnter, _trigger.LastColliderEnter.transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
