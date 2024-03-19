using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelScript : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("tutoHide", 0) == 1)
        {
            gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("tutoHide", 1);
    }
}
