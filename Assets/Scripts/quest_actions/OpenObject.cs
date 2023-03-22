using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObject : MonoBehaviour
{
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
        foreach(GameObject item in rewards)
        {
            try
            {
                item.gameObject.SetActive(true);
                //gives non criticall error on load if obj is already taken
            }
            catch
            {
                Destroy(this);
            }
        }
    }

    void SaveObjectData()
    {
        var SavedObject = new OpenableObjects();
        SavedObject.activated = true;
        SavedObject.objectPosition = transform.position;
        SaveGameManager.CurrentSaveData._openableObjects.Add(SavedObject);
        SaveGameManager.SaveGame();
    }
}

[System.Serializable]
public class OpenableObjects
{
    public bool activated;
    public Vector3 objectPosition;
}
