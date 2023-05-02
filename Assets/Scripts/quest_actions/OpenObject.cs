using System.Collections.Generic;
using UnityEngine;

public class OpenObject : MonoBehaviour
{
    Animator animator;
    //objects for one-click action
    public List<GameObject> rewards;

    private void OnEnable()
    {
        animator = gameObject.GetComponent<Animator>();
        for (int i = 0; i < SaveGameManager.CurrentSaveData._openableObjects.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._openableObjects[i].fullName == transform.name + transform.parent.name &&
                SaveGameManager.CurrentSaveData._openableObjects[i].activated == true)
            {
                animator.speed = 10;
                animator.SetTrigger("Active");
                print("found myself");
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
            if (SaveGameManager.CurrentSaveData._openableObjects[i].fullName == transform.name + transform.parent.name)
            {
                saveExists = true;
            }
        }
        if (!saveExists)
        {
            var SavedObject = new OpenableObjects();
            SavedObject.activated = true;
            SavedObject.fullName = transform.name + transform.parent.name;
            SaveGameManager.CurrentSaveData._openableObjects.Add(SavedObject);
            SaveGameManager.SaveGame();
        }
    }
}

[System.Serializable]
public class OpenableObjects
{
    public bool activated;
    public string fullName;
}
