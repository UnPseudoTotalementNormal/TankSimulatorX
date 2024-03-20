using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWallFadeScript : MonoBehaviour
{
    private Transform _playerTransform;
    private new Transform transform;

    private void Start()
    {
        transform = GetComponent<Transform>();
        _playerTransform = PlayerController.Instance.transform;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
    }
}
