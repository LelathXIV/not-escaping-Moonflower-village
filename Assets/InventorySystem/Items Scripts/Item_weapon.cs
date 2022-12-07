using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Weapon")]

public class Item_weapon : ItemObject
{
    public void Awake()
    {
        type = ItemType.weapon;
    }
}
