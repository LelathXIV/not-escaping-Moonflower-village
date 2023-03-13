using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    public float enemyCurrentHealth;
    [Inject] IPlayerWeapon playerWeapon;

    private void Update()
    {
        if(enemyCurrentHealth <= 0)
        {
            GetComponent<Enemy>().isAlive = false;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
        if(enemyCurrentHealth < enemyMaxHealth)
        {
            GetComponent<Enemy>().BattleModeOn();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "projectile")
        {
            enemyCurrentHealth -= playerWeapon.damageValue;
            print(enemyCurrentHealth);
        }
    }
}
