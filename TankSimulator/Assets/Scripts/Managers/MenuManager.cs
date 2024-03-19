using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

    [SerializeField] private TextMeshProUGUI _textCoins;

    private void Awake()
    {
        _fadeImage.color = Color.black;
    }

    void Start()
    {
        InputManager.Instance.ReleaseClickEvent.AddListener(TryStartGame);

        StartCoroutine(FadeIn());

        _textCoins.text = ": " + GameManager.Instance.Coins.ToString();
    }   

    private void TryStartGame()
    {
        if (InputManager.Instance.IsAiming())
        {
            GameManager.Instance.StartGame();
        }
    }

    private IEnumerator FadeIn()
    {
        while (_fadeImage.color.a > 0)
        {
            _fadeImage.color += new Color(0, 0, 0, -1 * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }
}
