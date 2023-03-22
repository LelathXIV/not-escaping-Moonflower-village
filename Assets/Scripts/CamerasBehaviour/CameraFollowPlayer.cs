using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;

    //makes awesome effect of switching camera, but after char goes out of cinemachine zone - camera smoothly follows player on respectable distance
    private void LateUpdate()
    {
        Vector3 newPosition = playerTransform.position + new Vector3(-3,2,0.5f);

        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
    }
}
