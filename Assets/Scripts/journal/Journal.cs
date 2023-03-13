using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Journal : MonoBehaviour
{
    public List<Journal_Page> listOfStoryPages;

    private void Start()
    {
        listOfStoryPages = SaveGameManager.CurrentSaveData._playerSavedStoryPages;
    }
    
    [ContextMenu("Clear Journal")]
    public void ClearJournal()
    {
        listOfStoryPages.Clear();
        SaveGameManager.SaveGame();
    }
    
    [ContextMenu("Clear Saved Journal")]
    public void ClearSavedJournal()
    {
        SaveGameManager.CurrentSaveData._playerSavedStoryPages.Clear();
        SaveGameManager.SaveGame();
    }
}

[System.Serializable]
public class Journal_Page
{
    public string questName;
    public List<JournalObj_story> thisStoryElements;
    public int pageNumber;
}

