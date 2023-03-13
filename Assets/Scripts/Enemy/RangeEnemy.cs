using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : Enemy
{
    public GameObject projectile;
    public float projectileSpeed;
    public float projectileLifetime;
    public float distance;
    private void Update()
    {
        if (isAlive)
        {
            isInFront();
            isInSight();
            if (isInSight() && isInFront() && !imFighting)
            {
                timerOfSight = 0;
                BattleModeOn();
                LookAtPlayer();
            }
            else if (!isInSight() && imFighting)
            {
                agent.SetDestination(playerGameobject.transform.position);
                timerOfSight += Time.deltaTime;
                if (timerOfSight >= followingPlayerIfOutOfSight)
                {
                    BattleModeOf();
                    ReturnToStartPosition();
                }
            }

            if (imFighting)
            {
                distance = Vector3.Distance(this.transform.position, playerGameobject.transform.position);
                if (distance <= attackRange && isInSight())
                {
                    agent.isStopped = true;
                    LookAtPlayer();
                    StartCoroutine(attac());
                }
                else agent.isStopped = false;
            }
        }
        else IsDead();
    }

    IEnumerator attac()
    {
        if (isDelaying != true)
        {
            {
                isAttacking = true;
                isDelaying = true;
                var originalColor = GetComponent<MeshRenderer>().material.color;
                GetComponent<MeshRenderer>().material.color = Color.red;
                agent.isStopped = true;
                yield return new WaitForSeconds(attacAnimationTime);
                agent.isStopped = false;
                Fire();
                GetComponent<MeshRenderer>().material.color = originalColor;
                isAttacking = false;
                StartCoroutine(ShootDelay());
            }
        }
    }

    public void Fire() 
    {
        GameObject bullet = Instantiate(projectile);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
        bullet.transform.position = transform.position;

        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, projectileLifetime));
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
