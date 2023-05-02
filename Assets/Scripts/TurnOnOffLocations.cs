using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOffLocations : MonoBehaviour
{
    public Transform locationObjectsParent;
    public List<GameObject> allLocations;
    public List<GameObject> toTurnOn;

    private void Start()
    {
        foreach (Transform location in locationObjectsParent)
        {
            if(location != null)
            {
                location.gameObject.SetActive(false);
                allLocations.Add(location.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TurnOnLocation();
        }
    }

    void TurnOnLocation()
    {
        for (int i = 0; i < allLocations.Count; i++)
        {
            allLocations[i].SetActive(false);
            if (toTurnOn.Count != 0)
            {
                for (int x = 0; x < toTurnOn.Count; x++)
                {
                    if (allLocations[i] == toTurnOn[x] && toTurnOn[x] != null)
                    {
                        toTurnOn[x].SetActive(true);
                    }
                }
            }
        }
    }

}
