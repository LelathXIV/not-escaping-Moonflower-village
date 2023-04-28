using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : Enemy
{
    public GameObject projectile;
    public float projectileLifetime;
    public float projectileSpeed;
    public float meleAttackDistance;
    public GameObject projectileParent;

    public void LateUpdate()
    {
        if (canAttack_heavy && distance > meleAttackDistance)
        {
            StartCoroutine(RangeAttack()); //heavy attk
            canAttack_heavy = false;
        }
        if (canAttack_heavy && distance < meleAttackDistance)
        {
            StartCoroutine(MeleAttack()); //simpleAttack
            canAttack_heavy = false;
        }
    }
    IEnumerator RangeAttack()
    {
        agent.isStopped = true;
        _heavyAttackCD = true;
        enemyAnimationController.Attack_1();
        isDelaying = true;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(ShootDelay());
        yield return new WaitForSeconds(simpleAttackCD);
        _heavyAttackCD = false;
        agent.isStopped = false;
        StopCoroutine(RangeAttack());
    }

    IEnumerator MeleAttack()
    {
        agent.isStopped = true;
        _simpleAttackCD = true;
        enemyAnimationController.Attack_2();
        isDelaying = true;
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(enemyAnimationController.interactionLength);
        float distanceNew = Vector3.Distance(this.transform.position, playerGameobject.transform.position);
        if (distanceNew <= meleAttackDistance)
        {
            playerGameobject.GetComponent<PlayerStats>().GetHit(attacDamageValue);
        }
        agent.isStopped = false;
        StartCoroutine(ShootDelay());
        yield return new WaitForSeconds(simpleAttackCD);
        _simpleAttackCD = false;
        StopCoroutine(MeleAttack());
    }

    public void Fire() 
    {
        GameObject bullet = Instantiate(projectile, projectileParent.transform);
        bullet.transform.position = projectileParent.transform.position;
        bullet.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        bullet.GetComponent<GolemProjectileBeh>().ProjectileSpeed = projectileSpeed;
        bullet.GetComponent<GolemProjectileBeh>().projectileDamage = attacDamageValue * heavyAtackMultiplier;
        StartCoroutine(DestroyBulletAfterTime(bullet, projectileLifetime));
    }

    
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
