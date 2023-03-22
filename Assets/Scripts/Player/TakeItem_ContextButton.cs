using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeItem_ContextButton : MonoBehaviour
{
    public InventoryObject playerInventory;
    public GameObject ContextImag_TakeItem;
    public int amount;
    public List<GameObject> itemsToCollect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "collectable")
        {
            print(other.tag);
            itemsToCollect.Add(other.gameObject);
            ContextImag_TakeItem.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "collectable") //change action icon and actions here
        {
            itemsToCollect.Remove(other.gameObject);

            ContextImag_TakeItem.gameObject.SetActive(false);
        }
    }

    public void PickUpItem()
    {
        //add pick-up effect here
        //сохранять состояние объекта в itemActionText
        foreach(GameObject item in itemsToCollect)
        {
            item.GetComponent<Items_InWorld>().AddToInventory();
        }
        itemsToCollect.Clear();
        ContextImag_TakeItem.gameObject.SetActive(false);
    }
}
