using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class WeaponSlot : MonoBehaviour
{
    public GameObject currentWeaponImage;
    public Image reloadVisual;
    public GameObject reloadVisualTimer;
    public float reloadingTime;
    public bool isReloading;

    public GameObject bulletsTotal;
    public GameObject magazineCapacity;
    private InventoryObject playerInventory;
    [Inject] IPlayerWeapon playerWeapon;
    private ItemObject currentWeapon;

    private void Start()
    {
        playerInventory = Resources.Load("PlayerInventory") as InventoryObject;
        if(SaveGameManager.CurrentSaveData._weaponIsEquiped == true)
        {
            for (int i = 0; i < playerInventory.Container.Count; i++)
            {
                if (playerInventory.Container[i].item.itemName == SaveGameManager.CurrentSaveData._playerEquippedWeapon)
                {
                    currentWeapon = playerInventory.Container[i].item;
                }
            }
            playerWeapon.GetWeapon(currentWeapon);
            Visual_MagazineCapacity();
            Visual_BulletsTotal(currentWeapon);
            AssignWeapon(currentWeapon);
        }
    }

    private void Update()
    {
        Visual_MagazineCapacity();
        if(isReloading == true)
        {
            reloadVisual.fillAmount = reloadingTime / playerWeapon.reload; //fuck math
            reloadingTime -= Time.deltaTime;
            reloadVisualTimer.GetComponent<TextMeshProUGUI>().text = reloadingTime.ToString("f2");
        }
    }

    public void AssignWeapon(ItemObject item)
    {
        playerWeapon.bulletsInMagazine = item.magazineCapacity;
        playerWeapon.GetWeapon(item);
        currentWeaponImage.GetComponent<Image>().sprite = item.prefabForInventory.GetComponent<Image>().sprite;
        Visual_BulletsTotal(item);
    }

    public void Visual_BulletsTotal(ItemObject item)
    {
        for (int i = 0; i < playerInventory.Container.Count; i++)
        {
            if (playerInventory.Container[i].item == item.bulletItem)
            {
                bulletsTotal.GetComponent<TextMeshProUGUI>().text = playerInventory.Container[i].amount.ToString();
            }
        }
    }

    public void Visual_MagazineCapacity()
    {
        if(playerWeapon.weaponIsEquiped == true)
        {
            magazineCapacity.GetComponent<TextMeshProUGUI>().text = playerWeapon.bulletsInMagazine.ToString();
        }
    }

    public void ReloadManually()
    {
        if(playerWeapon.weaponIsEquiped == true && playerWeapon.bulletsInMagazine < playerWeapon.magazineCapacity)
        {
            playerWeapon.Reloading();
        }
    }
    public void ReloadVisualsOn()
    {
        reloadingTime = playerWeapon.reload;
        reloadVisual.gameObject.SetActive(true);
        reloadVisualTimer.SetActive(true);
        isReloading = true;
    }

    public void RemoveBulletsFromInventoryOnReload(ItemObject item)
    {
        for (int i = 0; i < playerInventory.Container.Count; i++)
        {
            if (playerInventory.Container[i].item == item.bulletItem)
            {
                playerInventory.Container[i].RemoveAmount(playerWeapon.magazineCapacity);
            }
        }
    }

    public void ReloadVisualsOff()
    {
        reloadVisual.gameObject.SetActive(false);
        reloadVisualTimer.SetActive(false);
        isReloading = false;
    }
}

