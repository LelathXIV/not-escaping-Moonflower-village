using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    bullet,
    money,
    weapon,
    meleeWeapon,
    consumable,
    treasure,
    questObject,
    document
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefabForInventory;
    public ItemType type;

    public string itemName;
    [TextArea(15, 20)]
    public string description;
    public int sellPrice;
    public int buyPrice;

    //for consumables
    public int healingPotential;

    //for weapons
    public GameObject bulletPrefab;
    public ItemObject bulletItem;
    public int damageValue;
    public float reloadSpeed;
    public float bulletStootDelay;
    public int magazineCapacity;
    public float aimingTime;
    public float weaponRange;
}
