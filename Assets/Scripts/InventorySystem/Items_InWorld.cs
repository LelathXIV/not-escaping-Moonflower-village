using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_InWorld : MonoBehaviour
{
    //this script help all pickable(to inventory) object in scene identify themselves
    //add this script to all pickable objects

    public ItemObject item;
    public InventoryObject playerInventory;
    public int amount;
    PlayerSaveEvents playerSaveEvents;

    public void AddToInventory()
    {
        playerInventory.AddItem(item, amount);

        //add pick-up effect here
        GetComponent<ItemsInWorldLoader>().ObjectIsTaken();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSaveEvents>().SaveInventory();
        Destroy(gameObject);
    }
}
