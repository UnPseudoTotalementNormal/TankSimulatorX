using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxBackground : MonoBehaviour
{
    private RawImage _rawImage;
    private Transform _player;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
    }

    private void Start()
    {
        _player = PlayerController.Instance.transform;
    }

    private void Update()
    {
        _rawImage.uvRect = new Rect(0, _player.position.y / 2f, _rawImage.uvRect.width, _rawImage.uvRect.height);
    }
}
