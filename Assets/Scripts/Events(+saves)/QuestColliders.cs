using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class QuestColliders : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < SaveGameManager.CurrentSaveData._questCollidersSaveDatas.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._questCollidersSaveDatas[i].name == this.gameObject.name &&
                SaveGameManager.CurrentSaveData._questCollidersSaveDatas[i].expectedItemUsed == true)
                Destroy(this.gameObject); //for chests check if MG is finished;
        }
    }

    public GameObject targetObject; //with what object this collider should interact
    public ScriptableObject expectedItem; //key item to start action
    public bool expectedItemUsed; //is this action already finished
    public List<string> hints; //on click texts

    public void KeyItemUsed()
    {
        expectedItemUsed = true;
        SaveQuestColliderData();
        Destroy(transform.gameObject);
    }
    public void SaveQuestColliderData()
    {
        var QuestCollidersSaveData = new QuestCollidersSaveData();
        QuestCollidersSaveData.name = gameObject.name;
        QuestCollidersSaveData.expectedItemUsed = expectedItemUsed;
        SaveGameManager.CurrentSaveData._questCollidersSaveDatas.Add(QuestCollidersSaveData);
        SaveGameManager.SaveGame();
    }
}

[System.Serializable]
public class QuestCollidersSaveData
{
    public bool expectedItemUsed;
    public string name;
}
