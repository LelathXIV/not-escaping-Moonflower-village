using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    //store data here
    public PlayerSaveData _playerSaveData = new PlayerSaveData();

    //list of all takable gameobjects laying in the world
    public List<ObjectsInWorldSaveData> _listObWorldObjects = new List<ObjectsInWorldSaveData>();

    //list of action colliders
    public List<QuestCollidersSaveData> _questCollidersSaveDatas = new List<QuestCollidersSaveData>();

    //inventory list
    public List<InventorySaveData> _playerInventorySaveData = new List<InventorySaveData>();

    //saving journal
    public List<Journal_Page> _playerSavedStoryPages =  new List<Journal_Page>();

    //player equipped weapon
    public string _playerEquippedWeapon;
    public bool _weaponIsEquiped;

    //player MeleWeapon
    public string _playerMeleeWepEquipped;
    public bool _meleWepIsEquiped;

    //ancient chest MG data
    public List<AncientChestMGsSaveData> _ancientChestMGsSaveDatas = new List<AncientChestMGsSaveData>();

    //dialogue triggers
    public List<DialogueTriggersSaveData> _dialogueTriggersSaveData = new List<DialogueTriggersSaveData>();

    //list of objects that was already opened
    public List<OpenableObjects> _openableObjects = new List<OpenableObjects>();

    //list of quest objects in world
    public List<QuestObjectsSaveData> _questObjectsSaveData = new List<QuestObjectsSaveData>();

    //list on contextActionColliders

    public List<ContextQuestColliderSaveData> _contextQuestColliders = new List<ContextQuestColliderSaveData>();
}

