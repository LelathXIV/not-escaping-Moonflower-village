using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public static class SaveGameManager 
{
    public const string SaveDirectory = "/SaveData/";
    public const string FileName = "SaveGame.oops";

    public static SaveData CurrentSaveData = new SaveData();

    public static void SaveGame()
    {
        var dir = Application.persistentDataPath + SaveDirectory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string json = JsonUtility.ToJson(CurrentSaveData, true);
        File.WriteAllText(dir + FileName, json);

        GUIUtility.systemCopyBuffer = dir + FileName;

        Debug.Log("saved json " + json);
    }

    public static void LoadGame()
    {
        string fullPath = Application.persistentDataPath + SaveDirectory + FileName;
        SaveData tempData = new SaveData();
        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            tempData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log(json);
        }
        else
        {
            Debug.Log("save file does not exist");
        }

        CurrentSaveData = tempData;
    }

    public static void DeleteSaveGame()
    {
        string fullPath = Application.persistentDataPath + SaveDirectory + FileName;
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        else
        {
            Debug.Log("save file does not exist");
        }
    }
}

