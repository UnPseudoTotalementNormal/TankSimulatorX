using UnityEngine;

public class AdManager : MonoBehaviour
{

    private void Start()
    {
    }
    private void OnEndGame()
    {
        loadInterstitial.Instance.LoadAd();
    }
}
