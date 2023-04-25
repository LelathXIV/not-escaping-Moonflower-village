using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IPlayerWeapon
{
    public GameObject bulletPrefab { get; set; }
    public Transform bulletSpawn { get; set; }

    public float bulletSpeed = 30;

    public float lifeTime = 3;

    public float damageValue { get; set; }
    public float reload { get; set; }
    public float bulletShotDelay { get; set; }
    public bool isReloading { get; set; }
    public float aimingTime { get; set; }
    public bool weaponIsEquiped { get; set; }
    public int magazineCapacity { get; set; }
    public int bulletsInMagazine { get; set; }
    public int bulletsTotal { get; set; }
    public float weaponRange { get; set; }
    public ItemObject currentWeaponItem { get; set; }

    public void Fire() //get ITEM here
    {
       //  GameObject bullet = Instantiate(currentWeaponItem.bulletPrefab, new Quaternion());
         GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.LookRotation(bulletSpawn.transform.forward));
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());
         bullet.transform.position = bulletSpawn.position;

         bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
         bullet.GetComponent<BulletBehaviour>().DamageValue = damageValue;
         StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));
    }

    public void Reloading()
    {
        //кастить визуал в слот оружия
        isReloading = true;
        StartCoroutine(ReloadWeapon(currentWeaponItem));
        var weaponSlot = GameObject.FindGameObjectWithTag("weaponSlot").GetComponent<WeaponSlot>();
        weaponSlot.ReloadVisualsOn();
        Debug.Log("cast reloading");
    }

    public void GetWeapon(ItemObject item)
    {
        weaponIsEquiped = true;
        bulletPrefab = item.bulletPrefab;
        damageValue = item.damageValue;
        bulletShotDelay = item.bulletStootDelay;
        reload = item.reloadSpeed;
        magazineCapacity = item.magazineCapacity;
        currentWeaponItem = item;
        aimingTime = item.aimingTime;
        weaponRange = item.weaponRange;
        //there is a bug - on LoadGame always will be full magazine - aint gonna fix dat

        SaveGameManager.CurrentSaveData._playerEquippedWeapon = item.itemName;
        SaveGameManager.CurrentSaveData._weaponIsEquiped = true;
        SaveGameManager.SaveGame();
    }

    private IEnumerator ReloadWeapon(ItemObject item)
    {
        yield return new WaitForSeconds(item.reloadSpeed);
        Debug.Log("reloadED");
        isReloading = false;

        var player = GameObject.FindGameObjectWithTag("Player");
        var playerInventory = player.GetComponent<PlayerSaveEvents>().inventory;
        //removing bullets from inventory
        for (int i = 0; i < playerInventory.Container.Count; i++)
        {
            if (playerInventory.Container[i].item == item.bulletItem)
            {
                if (playerInventory.Container[i].amount < magazineCapacity)
                {
                    bulletsInMagazine = playerInventory.Container[i].amount;
                    playerInventory.Container.Remove(playerInventory.Container[i]);
                }
                else
                    playerInventory.Container[i].amount -= magazineCapacity;
            }
        }

        bulletsInMagazine = currentWeaponItem.magazineCapacity;
        var weaponSlot = GameObject.FindGameObjectWithTag("weaponSlot").GetComponent<WeaponSlot>();
        weaponSlot.Visual_BulletsTotal(currentWeaponItem);
        weaponSlot.ReloadVisualsOff();
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
