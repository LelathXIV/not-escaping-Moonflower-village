using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DisplayInventory : MonoBehaviour
{
    private InventoryObject inventory;
   [SerializeField] Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    public void Awake()
    {
        inventory = Resources.Load("PlayerInventory") as InventoryObject;
    }
    public void Start()
    {
        CreateDisplay();
    }

    public void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefabForInventory, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemsDisplayed.Add(inventory.Container[i], obj);
            obj.gameObject.name = inventory.Container[i].item.prefabForInventory.name;
        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                if (inventory.Container[i].amount == 1)
                {
                    itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefabForInventory, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
                obj.gameObject.name = inventory.Container[i].item.prefabForInventory.name;
            }
        }

        foreach (Transform child in transform)
        {
            bool itemInInventory = false;
            for (int i = 0; i < inventory.Container.Count; i++)
            {
                if (child.name == inventory.Container[i].item.prefabForInventory.name)
                { 
                    itemInInventory = true; 
                }
            }
            if (!itemInInventory)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
