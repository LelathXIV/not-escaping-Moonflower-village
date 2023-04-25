using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMeleeAttack_ContextButton : MonoBehaviour
{
    public GameObject meleeAttac;
    public MeleeWeaponSlot meleeWeaponSlot;
    public GameObject meleWepVisual;
    GameObject enemy;

    private void Update()
    {
        var ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (meleeWeaponSlot.isReloading)
            return;
        if (meleeWeaponSlot.meleeEquiped == true)
        {
            if (Physics.Raycast(ray, out hit, 1))
            {
                if (hit.transform.tag == "enemy" && meleeWeaponSlot.isReloading != true)
                {
                    enemy = hit.transform.gameObject;
                    if(enemy.GetComponent<Enemy>().isAlive)
                    {
                        meleeAttac.SetActive(true);
                    }
                }
                //add obstacles to SMASH here
            }
            else
            {
                meleeAttac.SetActive(false);
            }
        }
    }

    public void AttackEnemy()
    {
        meleeWeaponSlot.isReloading = true;
        meleeAttac.SetActive(false);
        GetComponent<PlayerAnimations>().MeleAttac();
        enemy.GetComponent<EnemyAnimationController>().TakeHit();
        StartCoroutine(ShowMeleWeapon());
        //add mele wep visual on/off
        var enemyBattle = enemy.GetComponent<Enemy>();
        if (enemyBattle.imFighting != true)
        {
            var damage = meleeWeaponSlot.currentMeleeWeapon.damageValue;
            var critDamage = Random.Range(damage * 2, damage * 3);
            enemy.GetComponent<EnemyHealth>().enemyCurrentHealth -= critDamage;
            enemyBattle.BattleModeOn();
            print(critDamage);
        }
        else { enemy.GetComponent<EnemyHealth>().enemyCurrentHealth -= meleeWeaponSlot.currentMeleeWeapon.damageValue; }
    }

    IEnumerator ShowMeleWeapon()
    {
        meleWepVisual.SetActive(true);
        GetComponent<PlayerShootingSystem>().StopAiming();
        yield return new WaitForSeconds(GetComponent<PlayerAnimations>().interactionLength);
        meleWepVisual.SetActive(false);
        StopAllCoroutines();
    }
}
    
    
