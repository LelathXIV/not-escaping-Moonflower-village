using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public Vector3 cameraOffset;

    public void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.transform.position + cameraOffset;
        transform.position = newPosition;
    }
}
