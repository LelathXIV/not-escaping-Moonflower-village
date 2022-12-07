using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerShootingSystem : MonoBehaviour
{
    [Inject] IPlayerWeapon playerWeapon;
    public bool weaponIsEquiped;
    public float damageValue;
    IEnumerator reload;

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

        if (weaponIsEquiped == true)
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.tag == "enemy" && playerWeapon.isReloading != true && playerWeapon.isDelaying != true)
                {
                    playerWeapon.Fire();

                    playerWeapon.isDelaying = true;

                    print(playerWeapon.isDelaying);
                    StartCoroutine(ShootDelay());
                }
            }
        }
    }

    public void GetWeapon(ItemObject item)
    {
        weaponIsEquiped = true;
        playerWeapon.bulletPrefab = item.bulletType;
        playerWeapon.damageValue = item.damageValue;
        damageValue = playerWeapon.damageValue;
        playerWeapon.bulletShotDelay = item.bulletStootDelay;
        playerWeapon.reload = item.reloadSpeed;
        //добавить замену картинки в слоте оружия
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(playerWeapon.bulletShotDelay);
        playerWeapon.isDelaying = false;
        print(playerWeapon.isDelaying);
    }

    //reload ammo
    //IEnumerator Reload()
    //{
    //
    //    yield return new WaitForSeconds(playerWeapon.reload);
    //
    //    playerWeapon.isReloading = false;
    //    Debug.Log(playerWeapon.isReloading);
    //
    //}
}
