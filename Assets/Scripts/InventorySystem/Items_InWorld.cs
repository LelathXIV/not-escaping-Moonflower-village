using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Items_InWorld : MonoBehaviour
{
    //this script help all pickable(to inventory) object in scene identify themselves
    //add this script to all pickable objects

    public ItemObject item;
    public InventoryObject playerInventory;
    public int amount;
    public GameObject pickedItemPanel;
    public GameObject canvas;
    private Transform pickedItemsImages;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("canvas");
        pickedItemsImages = canvas.transform.Find("pickedItemsImages");
    }
    public void AddToInventory()
    {
        
        var landedImg = Instantiate(pickedItemPanel, pickedItemsImages);
        landedImg.transform.Find("image").GetComponent<Image>().sprite = item.itemImage;
        if(amount > 1)
        { landedImg.transform.Find("amount").GetComponent<TextMeshProUGUI>().text = amount.ToString(); }

        playerInventory.AddItem(item, amount);
        GetComponent<ItemsInWorldLoader>().ObjectIsTaken();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSaveEvents>().SaveInventory();
        Destroy(gameObject);
    }
}
