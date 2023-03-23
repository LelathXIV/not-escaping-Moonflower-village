using System.Collections.Generic;
using UnityEngine;


public class QuestColliders : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject player;
    public GameObject targetObject; //with what object this collider should interact
    public ScriptableObject expectedItem; //key item to start action
    public bool expectedItemUsed; //is this action already finished
    public List<string> hints; //on click texts

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < SaveGameManager.CurrentSaveData._questCollidersSaveDatas.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._questCollidersSaveDatas[i].position == this.transform.position &&
                SaveGameManager.CurrentSaveData._questCollidersSaveDatas[i].expectedItemUsed == true)
                RunAnimation();
        }
    }

    public void CheckIfItemIsInInventory()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].item == expectedItem)
            {
                inventory.RemoveItem(inventory.Container[i].item, 1);
                player.GetComponent<PlayerSaveEvents>().SaveInventory();
                RunAnimation();
                KeyItemUsed();
            }
        }
    }

    public void RunAnimation()
    {
        targetObject.GetComponent<Animator>().SetTrigger("Active");
        Destroy(this.gameObject); //for check if MG is finished;
    }

    public void KeyItemUsed()
    {
        expectedItemUsed = true;
        SaveQuestColliderData();
        Destroy(transform.gameObject);
    }

    public void SaveQuestColliderData()
    {
        var QuestCollidersSaveData = new QuestCollidersSaveData();
        QuestCollidersSaveData.position = transform.position;
        QuestCollidersSaveData.expectedItemUsed = expectedItemUsed;
        SaveGameManager.CurrentSaveData._questCollidersSaveDatas.Add(QuestCollidersSaveData);
        SaveGameManager.SaveGame();
    }
}

[System.Serializable]
public class QuestCollidersSaveData
{
    public bool expectedItemUsed;
    public Vector3 position;
}
