using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.SpaceFighter;

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
    private ItemsInWorldLoader itemsInWorldLoader;
    public GameObject player;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("canvas");
        pickedItemsImages = canvas.transform.Find("pickedItemsImages");
        itemsInWorldLoader = GetComponent<ItemsInWorldLoader>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void AddToInventory()
    {
        var landedImg = Instantiate(pickedItemPanel, pickedItemsImages);
        landedImg.transform.Find("image").GetComponent<Image>().sprite = item.itemImage;
        if(amount > 1)
        { landedImg.transform.Find("amount").GetComponent<TextMeshProUGUI>().text = amount.ToString(); }

        playerInventory.AddItem(item, amount);
        itemsInWorldLoader.ObjectIsTaken();
        Destroy(gameObject);
    }
}
