using System.Collections;
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
    public Animator anim;
    public AnimationClip animationClip;
    public GameObject zoomUI;
    float timeBeforeSuicide;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = targetObject.GetComponent<Animator>();
        for (int i = 0; i < SaveGameManager.CurrentSaveData._questCollidersSaveDatas.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._questCollidersSaveDatas[i].position == this.transform.position &&
                SaveGameManager.CurrentSaveData._questCollidersSaveDatas[i].expectedItemUsed == true)
                RunAnimation();
        }
    }

    public void CheckIfItemIsInInventory()
    {
        if(expectedItem != null)
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
        //else - run hints method
    }

    public void RunAnimation()
    {
        timeBeforeSuicide = animationClip.length;
        anim.SetTrigger("Active");
        zoomUI.SetActive(false);
        StartCoroutine(Suicide());
    }

    IEnumerator Suicide()
    {
        yield return new WaitForSeconds(timeBeforeSuicide);
        zoomUI.SetActive(true);
        Destroy(this.gameObject);
    }

    public void KeyItemUsed()
    {
        expectedItemUsed = true;
        SaveQuestColliderData();
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
