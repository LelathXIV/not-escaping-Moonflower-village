using UnityEngine;

interface IPlayerWeapon 
{
    GameObject bulletPrefab { get; set; }
    Transform bulletSpawn { get; set; }
    float reload { get; set; }
    float bulletShotDelay { get; set; }
    float damageValue { get; set; }
    bool isReloading { get; set; }
    bool weaponIsEquiped { get; set; }
    int magazineCapacity { get; set; }
    int bulletsInMagazine { get; set; }
    int bulletsTotal { get; set; }
    float aimingTime { get; set; }
    float weaponRange { get; set; }
    void GetWeapon(ItemObject item);
    ItemObject currentWeaponItem { get; set; }
    void Fire();
    void Reloading();
}
