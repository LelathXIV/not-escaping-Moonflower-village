using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Bullet")]

public class Item_bullet : ItemObject
{
    public void Awake()
    {
        type = ItemType.bullet;
    }
}