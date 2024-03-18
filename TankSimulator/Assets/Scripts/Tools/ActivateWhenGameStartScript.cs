using System.Collections;
using UnityEngine;

public class ActivateWhenGameStartScript : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjectsToActivate;

    [SerializeField] GameObject[] gameObjectsToDeactivate;


    private void Start()
    {
        if (!GameManager.Instance.GameStarted)
        {
            foreach (GameObject gameObject in gameObjectsToActivate) { gameObject.SetActive(false); }

            foreach (GameObject gameObject in gameObjectsToDeactivate) { gameObject.SetActive(true); }

            GameManager.Instance.GameStartedEvent.AddListener(OnGameStart);
        }
        else
        {
            Debug.LogWarning("Game has already started");

            OnGameStart();
        }
    }

    private void OnGameStart()
    {
        foreach (GameObject gameObject in gameObjectsToActivate) { SwitchGameObject(gameObject, true); }

        foreach (GameObject gameObject in gameObjectsToDeactivate) { SwitchGameObject(gameObject, false); }

        GetComponent<ActivateWhenGameStartScript>().enabled = false;
    }

    private void SwitchGameObject(GameObject gObject, bool toState)
    {
        if (gObject.transform.TryGetComponent<CanvasGroup>(out CanvasGroup canvasGroup))
        {
            StartCoroutine(SwitchCanvasGroup(canvasGroup, toState));
        }
        else
        {
            gObject.SetActive(toState);
        }
    }

    private IEnumerator SwitchCanvasGroup(CanvasGroup canvasGroup, bool toState)
    {
        if (toState == false)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= 3 * Time.deltaTime;
                yield return null;
            }
            canvasGroup.gameObject.SetActive(false);
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.gameObject.SetActive(true);
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 3 * Time.deltaTime;
                yield return null;
            }
        }
        
        yield return null;
    }
}
