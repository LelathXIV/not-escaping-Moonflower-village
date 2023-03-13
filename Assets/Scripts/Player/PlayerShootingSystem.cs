using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class PlayerShootingSystem : MonoBehaviour
{
    [Inject] IPlayerWeapon playerWeapon;
    public float damageValue;
    public float aimingTimer;
    public GameObject aimingUI;
    public GameObject aimingLine;
    //wep info will be sent here

    private void Awake()
    {
        playerWeapon.bulletPrefab = Resources.Load("Prefabs/Revolver_Bullet_shoot") as GameObject;
        playerWeapon.bulletSpawn = transform.Find("BulletSpawn");
    }

    private void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);

        RaycastHit hit;

        if (playerWeapon.isReloading)
           return;
        //add aiming
        if (playerWeapon.weaponIsEquiped == true)
        {
            if (Physics.Raycast(ray, out hit, playerWeapon.weaponRange))
            {
                if (hit.transform.gameObject.tag == "enemy"  && playerWeapon.isReloading != true 
                     && playerWeapon.magazineCapacity > 0)
                {
                    aimingUI.SetActive(true);
                    aimingTimer += Time.deltaTime;
                    aimingLine.GetComponent<Image>().fillAmount = aimingTimer / playerWeapon.aimingTime;
                    if (aimingTimer >= playerWeapon.aimingTime)
                    {
                        playerWeapon.Fire();
                        aimingTimer = 0;
                        //reloading settings
                        playerWeapon.bulletsInMagazine -= 1;

                        if (playerWeapon.bulletsInMagazine <= 0)
                        {
                            playerWeapon.Reloading();
                        }
                    }
                }
              
            }
        }
    }
    public void StopAiming()
    {
        aimingTimer = 0;
        aimingUI.SetActive(false);
        aimingLine.GetComponent<Image>().fillAmount = 0;
    }
}
