using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralFloor : MonoBehaviour
{
    public GameObject totemSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<LukomoriePlayerHealth>().currentHealth = 0;
        }
    }
}
