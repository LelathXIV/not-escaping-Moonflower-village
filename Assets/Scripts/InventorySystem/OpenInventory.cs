using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject invVisualsHolder;
    public void OpenInventoryButton()
    {
        if (!inventory.activeInHierarchy)
        {
            inventory.SetActive(true);
        }
        else
            inventory.SetActive(false);
    }
}
