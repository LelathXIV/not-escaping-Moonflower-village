using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;


public class QuestColliders : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject player;
    public GameObject targetObject; //with what object this collider should interact
    public ScriptableObject expectedItem; //key item to start action
    public bool expectedItemUsed; //is this action already finished
    public List<string> hints; //on click texts
    public Animator anim;
    public AnimationClip animationClip;
    float timeBeforeSuicide;
    public List<GameObject> rewards;

    private void Start()
    {
        anim = targetObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < SaveGameManager.CurrentSaveData._questCollidersSaveDatas.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._questCollidersSaveDatas[i].finalPosition == transform.position &&
                SaveGameManager.CurrentSaveData._questCollidersSaveDatas[i].expectedItemUsed == true)
            {
                anim.speed = 10;
                RunAnimation();
            }
        }
    }

    public void CheckIfItemIsInInventory()
    {
        if (expectedItem != null)
        {
            for (int i = 0; i < inventory.Container.Count; i++)
            {
                if (inventory.Container[i].item == expectedItem)
                {
                    inventory.RemoveItem(inventory.Container[i].item, 1);
                    player.GetComponent<PlayerSaveEvents>().SaveInventory();
                    KeyItemUsed();
                    RunAnimation();
                }
            }
        }
        //else - run hints method
    }

    public void RunAnimation()
    {
        timeBeforeSuicide = animationClip.length;
        anim = targetObject.GetComponent<Animator>();
        anim.SetTrigger("Active");
        var playerZoom = player.GetComponent<Zoom_ContextButton>();
        if (playerZoom.isInZoom)
        {
            playerZoom.zoomUI.gameObject.SetActive(false);
        }
        StartCoroutine(Suicide());
    }

    IEnumerator Suicide()
    {
        yield return new WaitForSeconds(timeBeforeSuicide);
        var playerZoom = player.GetComponent<Zoom_ContextButton>();
        if (playerZoom.isInZoom)
        {
            playerZoom.zoomUI.gameObject.SetActive(true);
        }
        ShowRewards();
        Destroy(gameObject);
    }

    void ShowRewards()
    {
        for (int i = 0; i < rewards.Count; i++)
        {
            if (rewards[i] != null)
            rewards[i].SetActive(true);
        }
    }

    public void KeyItemUsed()
    {
        expectedItemUsed = true;
        SaveQuestColliderData();
    }

    public void SaveQuestColliderData()
    {
        var QuestCollidersSave = new QuestCollidersSaveData();
        QuestCollidersSave.finalPosition = transform.position;
        QuestCollidersSave.expectedItemUsed = expectedItemUsed;
        SaveGameManager.CurrentSaveData._questCollidersSaveDatas.Add(QuestCollidersSave);
        SaveGameManager.SaveGame();
    }
}

[System.Serializable]
public class QuestCollidersSaveData
{
    public bool expectedItemUsed;
    public Vector3 finalPosition;
}
