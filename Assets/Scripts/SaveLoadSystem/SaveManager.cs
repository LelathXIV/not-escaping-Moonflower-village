using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [ContextMenu("Save")]
    public void SaveGame()
    {
        SaveGameManager.SaveGame();
    }

    [ContextMenu("Load")]
    public void LoadGame()
    {
        //store load here
        SaveGameManager.LoadGame();
    }

    [ContextMenu("Delete Save Game")]
    public void DeleteSaveGame()
    {
        //store load here
        SaveGameManager.DeleteSaveGame();
    }

    private void Awake()
    {
        SaveGameManager.LoadGame();
    }
}
