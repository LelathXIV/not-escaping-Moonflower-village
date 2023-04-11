using UnityEngine;

public class PlayerSaveEvents : MonoBehaviour
{
    public InventoryObject inventory;

    public void Start()
    {
        inventory.Container.Clear();
        var itemsForInventory = Resources.LoadAll("ItemObject");
        for (int i = 0; i < SaveGameManager.CurrentSaveData._playerInventorySaveData.Count; i++)
        {
            foreach (ItemObject itemObject in itemsForInventory)
            {
                if (itemObject.name == SaveGameManager.CurrentSaveData._playerInventorySaveData[i].item_name)
                {
                    inventory.AddItem(itemObject, SaveGameManager.CurrentSaveData._playerInventorySaveData[i].amount);
                }
            }
        }
    }

    public void SaveInventory()
    {
        SaveGameManager.CurrentSaveData._playerInventorySaveData.Clear();

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var thisItemInList = new InventorySaveData();
            thisItemInList.item_name = inventory.Container[i].item.name;
            thisItemInList.amount = inventory.Container[i].amount;
            SaveGameManager.CurrentSaveData._playerInventorySaveData.Add(thisItemInList);
        }
        SaveGameManager.SaveGame();
    }
}

[System.Serializable]
public class InventorySaveData
{
    public string item_name;
    public int amount;
}
