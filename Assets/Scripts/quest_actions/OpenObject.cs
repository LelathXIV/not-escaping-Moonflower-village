using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class OpenObject : MonoBehaviour
{

    //objects for one-click action

    Animator animator;
    public List<GameObject> rewards;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        for (int i = 0; i < SaveGameManager.CurrentSaveData._openableObjects.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._openableObjects[i].objectPosition == transform.position &&
                SaveGameManager.CurrentSaveData._openableObjects[i].activated == true)
            {
                ShowRewards();
                animator.SetTrigger("Active");
            }
        }
    }

    public void RunAnimation()
    {
        animator.SetTrigger("Active");
        SaveObjectData();
    }

    public void ShowRewards()
    {               
        for (int i = 0; i < rewards.Count; i++)
        {
            if (rewards[i] == null)
            {
                rewards.Remove(rewards[i]);
            }
            else rewards[i].SetActive(true);
        }
    }

    void SaveObjectData()
    {
        var saveExists = false;
        for (int i = 0; i < SaveGameManager.CurrentSaveData._openableObjects.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._openableObjects[i].objectPosition == transform.position)
            {
                saveExists = true;
            }
        }
        if (!saveExists)
        {
            var SavedObject = new OpenableObjects();
            SavedObject.activated = true;
            SavedObject.objectPosition = transform.position;
            SaveGameManager.CurrentSaveData._openableObjects.Add(SavedObject);
            SaveGameManager.SaveGame();
        }
    }
}

[System.Serializable]
public class OpenableObjects
{
    public bool activated;
    public Vector3 objectPosition;
}
