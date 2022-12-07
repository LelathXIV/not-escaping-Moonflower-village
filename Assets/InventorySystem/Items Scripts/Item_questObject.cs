using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Quest Object")]

public class Item_questObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.questObject;
    }
}
