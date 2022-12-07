using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventory;
  

    public void OpenInventoryButton()
    {

        if (!inventory.active)
        {
            inventory.SetActive(true);
        }
        else
            inventory.SetActive(false);
    }
}
