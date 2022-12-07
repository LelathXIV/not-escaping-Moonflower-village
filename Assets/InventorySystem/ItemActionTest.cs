using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemActionTest : MonoBehaviour
{ 
    //this script help all pickable(to inventory) object in scene identify themselves
    //add this script to all pickable objects

	public ItemObject item;
	public InventoryObject playerInventory;

	public int amount;

	public void Awake()
	{
        var itemObjects = Resources.LoadAll("ItemObject");
		foreach(ItemObject _itemObject in itemObjects)
        {
			if(this.gameObject.name == _itemObject.prefabForScene.name)
            {
                item = _itemObject;
            }
        }
        playerInventory = Resources.Load("PlayerInventory") as InventoryObject;
    }

    public void AddToInventory()
    {
        playerInventory.AddItem(item, amount);

        //add pick-up effect here

        Destroy(gameObject);
    }
}
