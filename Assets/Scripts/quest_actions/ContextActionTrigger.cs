using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ContextActionTrigger : MonoBehaviour
{
    public GameObject targetObject;

    private void Start()
    {
        for (int i = 0; i < SaveGameManager.CurrentSaveData._contextQuestColliders.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._contextQuestColliders[i].position == this.transform.position &&
                SaveGameManager.CurrentSaveData._contextQuestColliders[i].executed == true)
                targetObject.GetComponent<Animator>().SetTrigger("Active");
                Destroy(this.gameObject); //for chests check if MG is finished;
        }
    }

    public void RunAction()
    {
        targetObject.GetComponent<Animator>().SetTrigger("Active");
        SaveQuestColliderData();
        Destroy(this.gameObject);
    }
    public void SaveQuestColliderData()
    {
        var QuestCollidersSaveData = new ContextQuestColliderSaveData();
        QuestCollidersSaveData.position = transform.position;
        QuestCollidersSaveData.executed = true;
        SaveGameManager.CurrentSaveData._contextQuestColliders.Add(QuestCollidersSaveData);
        SaveGameManager.SaveGame();
    }
}

[System.Serializable]
public class ContextQuestColliderSaveData
{
    public bool executed;
    public Vector3 position;
}
    



