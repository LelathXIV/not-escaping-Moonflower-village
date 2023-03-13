using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Melee weapon")]
public class Item_meleeWeapon : ItemObject
{
    private void Awake()
    {
        type = ItemType.meleeWeapon;
    }
}
