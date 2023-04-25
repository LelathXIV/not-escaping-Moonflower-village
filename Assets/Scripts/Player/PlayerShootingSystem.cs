using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class PlayerShootingSystem : MonoBehaviour
{
    [Inject] IPlayerWeapon playerWeapon;
    public float aimingTimer;
    public float damageValue;
    public bool isAiming;
    public GameObject aimingUI;
    public GameObject aimingLine;
    public float aimingSpeedBoost;
    public bool isReloading;
    int layerMask;
    public GameObject weaponVisual;

    private void Start()
    {
        playerWeapon.bulletPrefab = Resources.Load("Prefabs/Revolver_Bullet_shoot") as GameObject;
        playerWeapon.bulletSpawn = transform.Find("bulletSpawn");
        aimingSpeedBoost = GetComponent<PlayerStats>().aimingSpeedBoost;
        layerMask = LayerMask.GetMask("enemy");
    }

    private void Update()
    {
        isReloading = playerWeapon.isReloading;
        damageValue = playerWeapon.damageValue;
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (playerWeapon.weaponIsEquiped == true)
        {
            if (Physics.Raycast(ray, out hit, playerWeapon.weaponRange, layerMask))
            {
                if (hit.transform.gameObject.tag == "enemy" )
                {
                    Aiming(hit);
                }
                else
                { print("blind!");}
            }
        }
    }

    void Aiming(RaycastHit hit)
    {
        var enemy = hit.transform.gameObject;
        if (playerWeapon.isReloading != true &&
           enemy.GetComponent<Enemy>().isAlive && //does not see range enemy
           playerWeapon.magazineCapacity > 0)
        {
            isAiming = true;
            weaponVisual.SetActive(true);
            playerWeapon.bulletSpawn.gameObject.SetActive(true);
            aimingUI.SetActive(true);
            aimingTimer += Time.deltaTime;
            aimingLine.GetComponent<Image>().fillAmount = aimingTimer / playerWeapon.aimingTime;
            if (aimingTimer >= playerWeapon.aimingTime * aimingSpeedBoost)
            {
                Shoot();
            }
        }
    }
    void Shoot()
    {
        GetComponent<PlayerAnimations>().Shoot();
        playerWeapon.Fire();
        aimingTimer = 0;
        //reloading settings
        playerWeapon.bulletsInMagazine -= 1;
        if (playerWeapon.bulletsInMagazine <= 0)
        {
            playerWeapon.Reloading();
        }
    }
    public void StopAiming()
    {
        aimingTimer = 0;
        aimingUI.SetActive(false);
        aimingLine.GetComponent<Image>().fillAmount = 0;
        weaponVisual.SetActive(false);
        playerWeapon.bulletSpawn.gameObject.SetActive(false);
        isAiming = false;
    }
}
