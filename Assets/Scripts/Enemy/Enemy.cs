using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject playerGameobject;
    public EnemyAnimationController enemyAnimationController;
    public NavMeshAgent agent;
    public Vector3 StartPosition;
    public bool isDelaying;
    public bool imFighting;
    public bool isAlive;
    public float timerOfSight;
    public float followingTimer;
    public float viewFieldDistance;
    public float attackRange;
    public float attacDamageValue;
    public float attacDelay;

    public float simpleAttackCD;
    public bool _simpleAttackCD;
    public float heavyAttackCD; //for more abilities make dict of list
    public bool _heavyAttackCD;
    public float heavyAtackMultiplier;
    public float distance;
    public bool canAttack;
    public bool canAttack_simple;
    public bool canAttack_heavy;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerGameobject = GameObject.FindGameObjectWithTag("Player");
        isAlive = true;
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        StartPosition = transform.position;
    }

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
            if (imFighting == true && !isDelaying)
            {
                LookAtPlayer();

                if (_simpleAttackCD && _heavyAttackCD)
                {
                    agent.isStopped = true;
                    enemyAnimationController.Idle();
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
            if (!_heavyAttackCD && !isDelaying && !canAttack_heavy)
            {
                canAttack_heavy = true;
            }
            if (!_simpleAttackCD && _heavyAttackCD && !isDelaying && !canAttack_simple)
            {
                canAttack_simple = true;
            }
        }
    }

    public void GoToPlayer()
    {
        agent.SetDestination(playerGameobject.transform.position);
    }

    public void Idling()
    {
        isInFront();
        isInSight();
        enemyAnimationController.Idle();
    }

    public bool isInSight()
    {
      //RaycastHit hit;
      //PHYSYCS RAYCAST DOEST SEE CHARACTER CONTROLLER (>_<)`
      //Vector3 directionOfPlayer = playerGameobject.transform.position - transform.position;
      //if (Physics.Raycast(transform.position, directionOfPlayer, out hit, 5000f))
      //{
      //    if (hit.rigidbody.transform.tag == "Player")
      //    {
      //        Debug.DrawLine(transform.position, playerGameobject.transform.position, Color.red);
      //        return true;
      //    }
      //}
        if(Vector3.Distance(playerGameobject.transform.position, transform.position) < viewFieldDistance)
        {
            return true;
        }
        return false;
    }

    public bool isInFront()
    {
        Vector3 directionOfPlayer = transform.position - playerGameobject.transform.position;
        float angle = Vector3.Angle(transform.forward, directionOfPlayer);

        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            return true;
        }
        return false;
    }
    public void BattleModeOn()
    {
        if (!imFighting)
        {
            imFighting = true;
            var playerBattleMode = playerGameobject.GetComponent<BattleMode>();
            playerBattleMode.enemy = transform;
            playerBattleMode.listOfEnemies.Add(transform);
            LookAtPlayer();
        }
    }
    public void BattleModeOff()
    {
        imFighting = false;
        playerGameobject.GetComponent<BattleMode>().listOfEnemies.Remove(transform);
        timerOfSight = 0;
        StartCoroutine(GoHome());
    }

    IEnumerator GoHome()
    {
        var homeDirection = transform.parent.position;
        float time = 0;
        Vector3 startPosition = transform.position;
        transform.LookAt(homeDirection);
        while (time < 5)
        {
            enemyAnimationController.Run();
            transform.position = Vector3.Lerp(startPosition, homeDirection, time / 5);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = homeDirection;
    }

    public void IsDead()
    {
        playerGameobject.GetComponent<BattleMode>().listOfEnemies.Remove(transform);
        isAlive = false;
        agent.isStopped = true;
    }
    public void LookAtPlayer()
    {
        var damping = 5;
        var target = playerGameobject.transform;

        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
    public IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(attacDelay);
        isDelaying = false;
    }
}
