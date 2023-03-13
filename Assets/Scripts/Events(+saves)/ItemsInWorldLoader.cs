using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInWorldLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < SaveGameManager.CurrentSaveData._listObWorldObjects.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._listObWorldObjects[i].name == this.gameObject.name &&
                SaveGameManager.CurrentSaveData._listObWorldObjects[i].itemInWorldPosition == this.transform.position &&
                SaveGameManager.CurrentSaveData._listObWorldObjects[i].isTaken == true)
                         Destroy(this.gameObject);
        }
    }

    public void ObjectIsTaken()
    {
        var thisItem = new ObjectsInWorldSaveData();
        thisItem.isTaken = true;
        thisItem.name = this.gameObject.name;
        thisItem.itemInWorldPosition = this.transform.position;

        SaveGameManager.CurrentSaveData._listObWorldObjects.Add(thisItem);

        Destroy(this.gameObject);
    }
}
[System.Serializable]
public class ObjectsInWorldSaveData
{
    public string name;
    public Vector3 itemInWorldPosition;
    public bool isTaken;
}