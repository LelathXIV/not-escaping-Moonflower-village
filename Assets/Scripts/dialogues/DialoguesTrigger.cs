using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguesTrigger : MonoBehaviour
{
    public GameObject dialogUI;
    public GameObject inventoryUI;
    public bool isRepeatable;
    public bool isFinished;
    public List<string> dialogueLines;

    private void Start()
    {
        var saves = SaveGameManager.CurrentSaveData._dialogueTriggersSaveData;
        for (int i = 0; i < saves.Count; i++)
        {
            if (saves[i].coordinates == this.transform.position && !isRepeatable && saves[i].isFinished)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inventoryUI.SetActive(false);
            dialogUI.SetActive(true);
            dialogUI.gameObject.GetComponent<DialogueScript>().lines = dialogueLines;
            dialogUI.gameObject.GetComponent<DialogueScript>().StartDialogue();
            isFinished = true;
            gameObject.SetActive(false);
            SaveTriggerInfo();
        }
    }

    //fair for her self dialogues

    void SaveTriggerInfo()
    {
        var thisTriggerSaveData = new DialogueTriggersSaveData();
        thisTriggerSaveData.coordinates = gameObject.transform.position;
        thisTriggerSaveData.isFinished = true;
        SaveGameManager.CurrentSaveData._dialogueTriggersSaveData.Add(thisTriggerSaveData);
        SaveGameManager.SaveGame();
    }
}

[System.Serializable]
public class DialogueTriggersSaveData
{
    public Vector3 coordinates;
    public bool isFinished;
}

