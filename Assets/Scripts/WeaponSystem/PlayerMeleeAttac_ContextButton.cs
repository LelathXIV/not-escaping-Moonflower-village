using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttac_ContextButton : MonoBehaviour
{
    public GameObject meleeAttac;
    public MeleeWeaponSlot meleeWeaponSlot;
    GameObject enemy;
    private void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);

        RaycastHit hit;

        if (meleeWeaponSlot.isReloading)
            return;
        if (meleeWeaponSlot.meleeEquiped == true)
        {
            if (Physics.Raycast(ray, out hit, 1))
            {
                if (hit.transform.gameObject.tag == "enemy" && meleeWeaponSlot.isReloading != true)
                {
                    meleeAttac.SetActive(true);
                    enemy = hit.transform.gameObject;
                    Debug.Log(enemy);
                }
                //add obstacles to SMASH here
            }
            else
            {
                meleeAttac.SetActive(false);
            }
        }
    }

    public void Attac()
    {
        meleeWeaponSlot.isReloading = true;
        meleeAttac.SetActive(false);

        if (enemy.GetComponent<Enemy>().imFighting != true)
        {
            var damage = meleeWeaponSlot.currentMeleeWeapon.damageValue;
            var critDamage = Random.Range(damage * 2, damage * 3);
            enemy.GetComponent<EnemyHealth>().enemyCurrentHealth -= critDamage;
        }
        else { enemy.GetComponent<EnemyHealth>().enemyCurrentHealth -= meleeWeaponSlot.currentMeleeWeapon.damageValue; }
        Debug.Log(enemy.GetComponent<EnemyHealth>().enemyCurrentHealth);
        
    }
}
    
    
