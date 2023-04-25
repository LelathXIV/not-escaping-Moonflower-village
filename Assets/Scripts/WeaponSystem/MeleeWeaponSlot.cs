using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeleeWeaponSlot : MonoBehaviour
{
    public GameObject currentWeaponImage;
    public Image reloadVisual;
    public GameObject reloadVisualTimer;
    public float reloadingTime;
    public bool isReloading;
    public bool meleeEquiped;
    private InventoryObject playerInventory;
    public ItemObject currentMeleeWeapon;

    private void Start()
    {
        playerInventory = Resources.Load("PlayerInventory") as InventoryObject;
        if (SaveGameManager.CurrentSaveData._meleWepIsEquiped == true)
        {
            for (int i = 0; i < playerInventory.Container.Count; i++)
            {
                if (playerInventory.Container[i].item.itemName == SaveGameManager.CurrentSaveData._playerMeleeWepEquipped)
                {
                    currentMeleeWeapon = playerInventory.Container[i].item;
                }
            }
            AssignWeapon(currentMeleeWeapon);
        }
    }

    private void Update()
    {
        if (isReloading == true)
        {
            reloadVisualTimer.SetActive(true);
            reloadVisual.gameObject.SetActive(true);
            reloadVisual.fillAmount = reloadingTime / currentMeleeWeapon.reloadSpeed; //fuck math
            reloadingTime -= Time.deltaTime;
            reloadVisualTimer.GetComponent<TextMeshProUGUI>().text = reloadingTime.ToString("f2");
            if(reloadingTime <= 0)
            {
                StopReloading();
            }
        }
    }

    public void AssignWeapon(ItemObject item)
    {
        currentWeaponImage.GetComponent<Image>().sprite = item.prefabForInventory.GetComponent<Image>().sprite;
        currentMeleeWeapon = item;
        meleeEquiped = true;
        reloadingTime = item.reloadSpeed;
        SaveGameManager.CurrentSaveData._meleWepIsEquiped = true;
        SaveGameManager.CurrentSaveData._playerMeleeWepEquipped = currentMeleeWeapon.itemName;
        SaveGameManager.SaveGame();
    }

    public void StopReloading()
    {
        isReloading = false;
        reloadingTime = currentMeleeWeapon.reloadSpeed;
        reloadVisualTimer.SetActive(false);
        reloadVisual.gameObject.SetActive(false);
    }
}
