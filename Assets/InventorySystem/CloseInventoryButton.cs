using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CloseInventoryButton : MonoBehaviour
{
    [Inject] IGamePauseService gamePauseService;  

    public void CloseInventory()
    {
        transform.parent.gameObject.SetActive(false);
        gamePauseService.UnFreeze();
    }
}
