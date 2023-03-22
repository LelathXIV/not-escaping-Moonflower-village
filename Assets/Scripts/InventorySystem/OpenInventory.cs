using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventory;
    [Inject] IGamePauseService gamePause;
    public void OpenInventoryButton()
    {
        if (!inventory.activeInHierarchy)
        {
            inventory.SetActive(true);
            gamePause.Freeze();
        }
        else
        {
            inventory.SetActive(false);
            gamePause.UnFreeze();
        }
    }
}
