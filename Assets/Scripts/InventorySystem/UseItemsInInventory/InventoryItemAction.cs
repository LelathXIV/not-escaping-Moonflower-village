using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

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
    private Transform destroyObjectButton;
    private string destroyItemText;
    private Button yesDestroyButton;
    private Button noDestroyButton;
    private Transform confirmDestroyPanel;

    private GameObject player;

    private void Awake()
    {
        btn = gameObject.AddComponent<Button>();
        btn.onClick.AddListener(ActionOnClick);
        darkeningPanel = Resources.Load("Prefabs/PanelsPrefabs/DarkeningPanel") as GameObject;
        itemDescriptionPrefab = Resources.Load("Prefabs/PanelsPrefabs/ObjectContextPanel") as GameObject;
    }

    public void Start()
    {
        playerInventory = Resources.Load("PlayerInventory") as InventoryObject;
        player = GameObject.FindGameObjectWithTag("Player");

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
        var parent = Instantiate(darkeningPanel, this.transform.parent.transform.parent.transform.parent.transform);
        parent.transform.SetAsLastSibling();
        parent.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);

        var contextMenuPrefab = Instantiate(itemDescriptionPrefab, parent.transform);
        contextMenuPrefab.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);

        contextMenuPrefab.transform.Find("object2DImage").GetComponent<Image>().sprite = item.prefabForInventory.GetComponent<Image>().sprite;
        contextMenuPrefab.transform.Find("objectLoreName").GetComponent<TextMeshProUGUI>().text = item.itemName;

        SwitchTexts();
        destroyItemText = "throw away".ToString();

        contextMenuPrefab.transform.Find("objectMainStats").GetComponent<TextMeshProUGUI>().text = itemStatsText;
        contextMenuPrefab.transform.Find("objectLoreDescription").GetComponent<TextMeshProUGUI>().text = item.description;

        confirmDestroyPanel = contextMenuPrefab.transform.Find("confirmDestroy");
        confirmDestroyPanel.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "Are you sure you" + "\n" + "want to destroy " + item.itemName + "?";
        yesDestroyButton = confirmDestroyPanel.transform.Find("yes").GetComponent<Button>();
        noDestroyButton = confirmDestroyPanel.transform.Find("no").GetComponent<Button>();

        useObjectButton = contextMenuPrefab.transform.Find("UseObjectButton");
        useObjectButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = contextButtontext;
        useObjectButton.gameObject.GetComponent<Button>().onClick.AddListener(UseObject);

        destroyObjectButton = contextMenuPrefab.transform.Find("DestroyObject");
        destroyObjectButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = destroyItemText;
        destroyObjectButton.gameObject.GetComponent<Button>().onClick.AddListener(DestroyObject);

    }

    private void UseObject()
    {
        //разделить на кейсы!! хилит/эквип и пр
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

        }
    }

    private void DestroyObject()
    {
        confirmDestroyPanel.gameObject.SetActive(true);
        yesDestroyButton.onClick.AddListener(YesDestroy);
        noDestroyButton.onClick.AddListener(NoDestroy);
    }

    private void YesDestroy()
    {
        playerInventory.RemoveItem(item, int.MaxValue);
        confirmDestroyPanel.transform.parent.transform.parent.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private void NoDestroy()
    {
        confirmDestroyPanel.gameObject.SetActive(false);
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
                //description
                break;
            case ItemType.document:
                //description
                break;
        }
    }
}
