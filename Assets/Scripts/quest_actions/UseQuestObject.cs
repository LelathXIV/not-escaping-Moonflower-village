using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class UseQuestObject : MonoBehaviour
{
    public bool keyUsed;
    Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        for (int i = 0; i < SaveGameManager.CurrentSaveData._questObjectsSaveData.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._questObjectsSaveData[i].objectPosition == transform.position &&
                SaveGameManager.CurrentSaveData._questObjectsSaveData[i].activated == true)
            {
                animator.SetTrigger("Active");
            }
        }
    }

    public void SaveObjectData()
    {
        var saveExists = false;
        for (int i = 0; i < SaveGameManager.CurrentSaveData._questObjectsSaveData.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._questObjectsSaveData[i].objectPosition == transform.position)
            {
                saveExists = true;
            }
        }
        if(!saveExists)
        {
            var SavedObject = new QuestObjectsSaveData();
            SavedObject.activated = true;
            SavedObject.objectPosition = transform.position;
            SaveGameManager.CurrentSaveData._questObjectsSaveData.Add(SavedObject);
            SaveGameManager.SaveGame();
        }
    }
}

[System.Serializable]
public class QuestObjectsSaveData
{
    public bool activated;
    public Vector3 objectPosition;
}
