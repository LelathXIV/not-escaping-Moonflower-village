using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerWeapon : MonoBehaviour, IPlayerWeapon
{
    public GameObject bulletPrefab { get; set; }
    public Transform bulletSpawn { get; set; }

    public float bulletSpeed = 30;

    public float lifeTime = 3;

    public float damageValue { get; set; }
    public float reload { get; set; }

    public float bulletShotDelay { get; set; }
    public bool isDelaying { get; set; }

    public bool isReloading { get; set; }

    public void Fire()
    {
         GameObject bullet = Instantiate(bulletPrefab);
         Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());
         bullet.transform.position = bulletSpawn.position;
         
         Vector3 rotation = bullet.transform.rotation.eulerAngles;
         bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
         bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
         
         StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
