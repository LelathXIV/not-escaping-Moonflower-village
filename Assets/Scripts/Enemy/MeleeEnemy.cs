using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class MeleeEnemy : Enemy
{
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
                timerOfSight += Time.deltaTime;
                if (timerOfSight >= followingPlayerIfOutOfSight)
                {
                    BattleModeOf();
                    ReturnToStartPosition();
                }
            }
            if(imFighting)
            {
                float distance = Vector3.Distance(this.transform.position, playerGameobject.transform.position);
                agent.SetDestination(playerGameobject.transform.position);
                if (distance <= attackRange)
                    StartCoroutine(attac());
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
                float distanceNew = Vector3.Distance(this.transform.position, playerGameobject.transform.position);
                if (distanceNew <= attackRange)
                {
                    playerGameobject.GetComponent<PlayerHealth>().playerCurrentHealth -= attacDamageValue;
                    print(playerGameobject.GetComponent<PlayerHealth>().playerCurrentHealth);
                }
                agent.isStopped = false;
                GetComponent<MeshRenderer>().material.color = originalColor;
                isAttacking = false;
                StartCoroutine(ShootDelay());
            }
        }
    }
}