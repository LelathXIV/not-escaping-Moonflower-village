using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetPosition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.SetActive(false);
            other.transform.position = targetPosition.position;
            print(targetPosition.position);
            other.gameObject.SetActive(true);
        }
    }
}
