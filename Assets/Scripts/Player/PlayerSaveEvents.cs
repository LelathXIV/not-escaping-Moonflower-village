using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveEvents : MonoBehaviour
{
    public InventoryObject inventory;
    public int SceneNumber;
    public bool isInLukomorie;
    public Transform thisLocationTotem;

    private void Awake()
    {
        if (thisLocationTotem != null)
            RelocatePlayerToTotem();
    }

    public void Start()
    {
        inventory.Container.Clear();
        var itemsForInventory = Resources.LoadAll("ItemObject");
        var save = SaveGameManager.CurrentSaveData;
        for (int i = 0; i < save._playerInventorySaveData.Count; i++)
        {
            foreach (ItemObject itemObject in itemsForInventory)
            {
                if (itemObject.name == save._playerInventorySaveData[i].item_name)
                {
                    inventory.AddItem(itemObject, save._playerInventorySaveData[i].amount);
                }
            }
        }
    }

    void RelocatePlayerToTotem()
    {
        gameObject.SetActive(false);
        transform.position = thisLocationTotem.position;
        gameObject.SetActive(true);
    }

    public void GetSceneNumber()
    {
        Scene scene = SceneManager.GetActiveScene();

        switch (scene.name)
        {
            case ("Start_scene"):
                SceneNumber = 0;
                break;
            case ("Oak_scene"):
                SceneNumber = 1;
                break;
            case ("Lukomorie"):
                SceneNumber = 2;
                break;
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
    public void SavePlayerScene()
    {
        SaveGameManager.CurrentSaveData.currentlyPlayableScene = SceneNumber;
        SaveGameManager.CurrentSaveData.isInAstral = isInLukomorie;
        SaveGameManager.SaveGame();
    }
}

[System.Serializable]
public class InventorySaveData
{
    public string item_name;
    public int amount;
}

