using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class OpenDoor : MonoBehaviour
{
    public Animator animator;
    public List<GameObject> listOfTriggers;
    public GameObject zoomCollider;
    public GameObject player;
    public bool isKeyDoor;
    public bool isOpened;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        var doorsData = SaveGameManager.CurrentSaveData._doorsSaveData;
        for (int i = 0; i < doorsData.Count; i++)
        {
            if (doorsData[i].finalPosition == transform.position &&
                doorsData[i].isOpened == true)
            {
                animator.speed = 10;
                isOpened = true;
                RunAnimation();
            }
        }
    }
    //checks how many action colliders(keyholes) still exists
    //if 0 - destroy triggers - lock is opened
    private void Update()
    {
        if(isKeyDoor && !isOpened)
        {
            for (int i = 0; i < listOfTriggers.Count; i++)
            {
                if (listOfTriggers[i] == null)
                {
                    listOfTriggers.Remove(listOfTriggers[i]);
                }
            }
            if (listOfTriggers.Count == 0)
            {
                RunAnimation();
                isOpened = true;
                SaveQuestColliderData();
            }
        }
    }
    public void RunAnimation()
    {
        animator.SetTrigger("Active");
        zoomCollider.GetComponent<Collider>().enabled = false;
        var playerZoom = player.GetComponent<Zoom_ContextButton>();
        if (playerZoom.isInZoom)
        {
            playerZoom.zoomUI.gameObject.SetActive(false);
        }
    }

    public void CheckZoomStatus()
    {
        var playerZoom = player.GetComponent<Zoom_ContextButton>();
        if (playerZoom.isInZoom)
        {
            playerZoom.zoomUI.gameObject.SetActive(true);
            playerZoom.ClearContextActionWindow();
        }
    }

    public void SaveQuestColliderData()
    {
        var doors = new DoorsSaveData();
        doors.finalPosition = transform.position;
        doors.isOpened = isOpened;
        SaveGameManager.CurrentSaveData._doorsSaveData.Add(doors);
        SaveGameManager.SaveGame();
    }
}
[System.Serializable]
public class DoorsSaveData
{
    public bool isOpened;
    public Vector3 finalPosition;
}

