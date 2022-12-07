using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera thisCamera;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            thisCamera.Priority = 1;
            thisCamera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            thisCamera.Priority = 0;
            thisCamera.gameObject.SetActive(false);
        }
    }
}
