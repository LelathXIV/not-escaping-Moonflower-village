using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;
    public List<GameObject> listOfTriggers;
    public GameObject theDoorObject;
    public GameObject player;
    public GameObject zoomCollider;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        for (int i = 0; i < SaveGameManager.CurrentSaveData._openableObjects.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._openableObjects[i].objectPosition == transform.position &&
                SaveGameManager.CurrentSaveData._openableObjects[i].activated == true)
            {
                animator.SetTrigger("Active");
            }
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        for (int i = 0; i < listOfTriggers.Count; i++)
        {
            if(listOfTriggers[i] == null)
            {
                listOfTriggers.Remove(listOfTriggers[i]);
            }
        }
        if(listOfTriggers.Count == 0)
        {
            RunAnimation();
        }
    }

    public void RunAnimation()
    {
        animator.SetTrigger("Active");
        SaveObjectData();
    }

    //to prevent door moving over player object
    public void MeshOn()
    {
        theDoorObject.GetComponent<MeshCollider>().enabled = true;
        player.GetComponent<Zoom_ContextButton>().CloseZoom();
        zoomCollider.gameObject.SetActive(false);
    }

    public void MeshOff()
    {
        theDoorObject.GetComponent<MeshCollider>().enabled = false;
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
