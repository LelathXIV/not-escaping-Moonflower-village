using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using Zenject.SpaceFighter;
using System.Collections.Generic;

public class InventoryItemAction : MonoBehaviour
{
    public ItemObject item;
    public InventoryObject playerInventory;
    public Button btn;
    private GameObject darkeningPanel;
    private GameObject itemDescriptionPrefab;
    private string itemStatsText;
    private Transform useObjectButton;
    private string contextButtontext;

    private void Awake()
    {
        btn = gameObject.AddComponent<Button>();
        btn.onClick.AddListener(ActionOnClick);
        darkeningPanel = Resources.Load("Prefabs/PanelsPrefabs/DarkeningPanel") as GameObject;
        itemDescriptionPrefab = Resources.Load("Prefabs/PanelsPrefabs/ObjectContextPanel") as GameObject;
        //currentlyActiveCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().activeCamera;
    }

    public void Start()
    {
        playerInventory = Resources.Load("PlayerInventory") as InventoryObject;

        for (int i = 0; i < playerInventory.Container.Count; i++)
        {
            if (gameObject.name == playerInventory.Container[i].item.prefabForInventory.name)
            {
                item = playerInventory.Container[i].item;
            }
        }
    }

        //shorten it somehow (later keeek)
    public void ActionOnClick()
    {
        //pause game - bug?
        var parent = Instantiate(darkeningPanel, this.transform.parent.transform.parent.transform.parent.transform); //adapt it later
        parent.transform.SetAsLastSibling();
        parent.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);

        var contextMenuPrefab = Instantiate(itemDescriptionPrefab, parent.transform);
        contextMenuPrefab.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);

        contextMenuPrefab.transform.Find("object2DImage").GetComponent<Image>().sprite = item.prefabForInventory.GetComponent<Image>().sprite;
        contextMenuPrefab.transform.Find("objectLoreName").GetComponent<TextMeshProUGUI>().text = item.itemName;

        SwitchTexts();

        contextMenuPrefab.transform.Find("objectMainStats").GetComponent<TextMeshProUGUI>().text = itemStatsText;
        contextMenuPrefab.transform.Find("objectLoreDescription").GetComponent<TextMeshProUGUI>().text = item.description;

        useObjectButton = contextMenuPrefab.transform.Find("UseObjectButton");
        useObjectButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = contextButtontext;
        useObjectButton.gameObject.GetComponent<Button>().onClick.AddListener(UseObject);


        //inactivate UseObject for quest items e.t.c
    }

    private void UseObject()
    {
        switch (item.type)
        {
            case ItemType.weapon:

                GameObject.FindGameObjectWithTag("weaponSlot").GetComponent<WeaponSlot>().AssignWeapon(item);
                Destroy(useObjectButton.gameObject.transform.parent.transform.parent.gameObject);
                break;
            case ItemType.meleeWeapon:
                GameObject.FindGameObjectWithTag("meleeWeaponSlot").GetComponent<MeleeWeaponSlot>().AssignWeapon(item);
                Destroy(useObjectButton.gameObject.transform.parent.transform.parent.gameObject);
                break;
            case ItemType.questObject:
                useObjectButton.gameObject.SetActive(false);
                break;
        }
        useObjectButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void SwitchTexts()
    {
        switch (item.type)
        {
            case ItemType.bullet:
                itemStatsText = ("Sell price" + item.sellPrice).ToString();
                break;
            case ItemType.consumable:
                itemStatsText = ("Healing power: " + item.healingPotential + "\n" +
                                 "Sell price: " + item.sellPrice
                               ).ToString();
                contextButtontext = "Heal";
                break;
            case ItemType.money:
                //smth
                break;
            case ItemType.weapon:
                itemStatsText = ("Damage power: " + item.healingPotential + "\n" +
                                 "Reload speed: " + item.reloadSpeed + "\n" +
                                 "Sell price:   " + item.sellPrice
                                 ).ToString();
                contextButtontext = "Equip";

                break;
            case ItemType.treasure:
                //description
                break;
            case ItemType.questObject:
                contextButtontext = "Use";
                break;
            case ItemType.document:
                //description
                break;
        }
    }
}
