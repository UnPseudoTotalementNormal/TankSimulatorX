using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public static HUDScript Instance;

    [SerializeField] private TextMeshProUGUI _coinText;

    [SerializeField] private Image _fadeImage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.GameEndedEvent.AddListener(OnGameEnded);
    }

    private void Update()
    {
        _coinText.text = "Coins: " + GameManager.Instance.Coins.ToString();
    }

    private void OnGameEnded()
    {
        StartCoroutine(EndGameAnimation());
    }

    private IEnumerator EndGameAnimation()
    {
        _fadeImage.color = new Color(0, 0, 0, 0);
        while (_fadeImage.color.a < 1)
        {
            _fadeImage.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
            yield return null;
        }
        
        yield return null;
    }
}
