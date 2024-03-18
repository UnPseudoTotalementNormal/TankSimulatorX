using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

    private void Awake()
    {
        _fadeImage.color = Color.black;
    }

    void Start()
    {
        InputManager.Instance.ReleaseClickEvent.AddListener(TryStartGame);

        StartCoroutine(FadeIn());
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
