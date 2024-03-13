using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private int _oldsec;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_oldsec != (int)Time.time)
        {
            _text.text = "FPS: " + (1f / Time.deltaTime).ToString();
            _oldsec = (int)Time.time;
        }
        
    }
}
