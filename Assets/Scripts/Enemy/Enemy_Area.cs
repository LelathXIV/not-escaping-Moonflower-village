using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Area : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }

}
