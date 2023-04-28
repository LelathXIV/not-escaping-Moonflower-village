using System.Collections;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MeleeEnemy : Enemy
{
    public void LateUpdate()
    {
        if(canAttack_heavy)
        {
            StartCoroutine(HeavyAtack());
            canAttack_heavy = false;
        }
        if(canAttack_simple)
        {
            StartCoroutine(SimpleAtack());
            canAttack_simple = false;
        }
    }

    IEnumerator SimpleAtack()
    {
        agent.isStopped = true;
        _simpleAttackCD = true;
        enemyAnimationController.Attack_1();
        isDelaying = true;
        yield return new WaitForSeconds(1.5f);
        float distanceNew = Vector3.Distance(this.transform.position, playerGameobject.transform.position);
        if (distanceNew < attackRange && isInFront())
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
        enemyAnimationController.Attack_2();
        yield return new WaitForSeconds(1.5f);
        print("can't read animation length");
        float distanceNew = Vector3.Distance(this.transform.position, playerGameobject.transform.position);
        if (distanceNew < attackRange)
        {
            playerGameobject.GetComponent<PlayerStats>().GetHit(attacDamageValue * heavyAtackMultiplier);
        }
        agent.isStopped = false;
        StartCoroutine(ShootDelay());
        yield return new WaitForSeconds(heavyAttackCD);
        _heavyAttackCD = false;
        StopCoroutine(HeavyAtack());
    }

}