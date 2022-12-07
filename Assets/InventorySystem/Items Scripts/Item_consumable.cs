using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Consumable")]

public class Item_consumable : ItemObject
{
    public void Awake()
    {
        type = ItemType.consumable;
    }
}
