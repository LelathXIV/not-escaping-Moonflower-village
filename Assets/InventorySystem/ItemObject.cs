using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    bullet,
    money,
    weapon,
    consumable,
    treasure,
    questObject,
    document
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefabForInventory;
    public GameObject prefabForScene;
    public ItemType type;
    public int ID;

    public string itemName;
    [TextArea(15, 20)]
    public string description;
    public int sellPrice;
    public int buyPrice;

    //for weapons
    public int damageValue;
    public int reloadSpeed;
    public GameObject bulletType;
    public float bulletStootDelay;

    //for consumables
    public int healingPotential;
}
