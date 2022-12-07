using UnityEngine;

interface IPlayerWeapon 
{
    GameObject bulletPrefab { get; set; }
    Transform bulletSpawn { get; set; }
    float reload { get; set; }
    float bulletShotDelay { get; set; }
    float damageValue { get; set; }
    bool isReloading { get; set; }
    bool isDelaying { get; set; }
    void Fire();
}
