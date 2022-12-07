using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAtPlayer : MonoBehaviour
{
    private Camera camera;
    private GameObject player;

    private void Awake()
    {
        camera = this.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        camera.transform.LookAt(player.transform);
    }
}
