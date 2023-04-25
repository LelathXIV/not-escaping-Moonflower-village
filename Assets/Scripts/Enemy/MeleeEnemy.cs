using System.Collections;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MeleeEnemy : Enemy
{
    public float simpleAttackCD;
    public bool _simpleAttackCD;
    public float heavyAttackCD; //for more abilities make dict of list
    public bool _heavyAttackCD;
    public float heavyAtackMultiplier;
    float distance;
    private void Update()
    {
        if (isAlive)
        {
            distance = Vector3.Distance(this.transform.position, playerGameobject.transform.position);

            Idling(); //add idle route
            if (isInSight() && isInFront() && !imFighting)
            {
                timerOfSight = 0;
                BattleModeOn();
                LookAtPlayer();
            }
            else if (!isInSight() && imFighting)
            {
                timerOfSight += Time.deltaTime;
                if (timerOfSight >= followingTimer)
                {
                    timerOfSight = 0;
                    agent.isStopped = true;
                    BattleModeOff();
                }
            }
            if(imFighting == true && !isDelaying)
            {
                if (_simpleAttackCD && _heavyAttackCD)
                {
                    agent.isStopped = true;
                    enemyAnimationController.Idle();
                    LookAtPlayer();
                }
                else 
                {
                    if (distance > attackRange)
                    {
                        GoToPlayer();
                        enemyAnimationController.Run();
                        agent.isStopped = false;
                    }
                    AttacAbilities();
                }
            }
        }
        else IsDead();
    }  

    void AttacAbilities()
    {
        if (distance <= attackRange)
        {
            if (!_heavyAttackCD)
            {
                StartCoroutine(HeavyAtack());
            }
            if (!_simpleAttackCD && _heavyAttackCD && !isDelaying)
            { StartCoroutine(SimpleAtack()); }
        }
    }

    void GoToPlayer()
    {
        agent.SetDestination(playerGameobject.transform.position);
    }

    IEnumerator SimpleAtack()
    {
        agent.isStopped = true;
        _simpleAttackCD = true;
        enemyAnimationController.Attack_1();
        isDelaying = true;
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(enemyAnimationController.interactionLength);
        float distanceNew = Vector3.Distance(this.transform.position, playerGameobject.transform.position);
        if (distanceNew <= attackRange)
        {
            playerGameobject.GetComponent<PlayerStats>().GetHit(attacDamageValue);
        }
        agent.isStopped = false;
        StartCoroutine(ShootDelay());
        yield return new WaitForSeconds(simpleAttackCD);
        _simpleAttackCD = false;
        StopCoroutine(SimpleAtack());
    }

    IEnumerator HeavyAtack()
    {
        agent.isStopped = true;
        _heavyAttackCD = true;
        isDelaying = true;
        _simpleAttackCD = true;
        enemyAnimationController.Attack_2();
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(enemyAnimationController.interactionLength);
        float distanceNew = Vector3.Distance(this.transform.position, playerGameobject.transform.position);
        if (distanceNew <= attackRange)
        {
            playerGameobject.GetComponent<PlayerStats>().GetHit(attacDamageValue * heavyAtackMultiplier);
        }
        agent.isStopped = false;
        _simpleAttackCD = false;
        StartCoroutine(ShootDelay());
        yield return new WaitForSeconds(heavyAttackCD);
        _heavyAttackCD = false;
        StopCoroutine(HeavyAtack());
    }

}