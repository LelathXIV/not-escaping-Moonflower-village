using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject playerGameobject;
    public NavMeshAgent agent;
    public Vector3 startPosition;
    public bool isDelaying;
    public bool imFighting;
    public bool isAttacking;
    public bool isAlive;
    public float timerOfSight;
    public float followingPlayerIfOutOfSight;
    public float viewFieldDistance;
    public float attackRange;
    public float attacDamageValue;
    public float attacDelay;
    public float attacAnimationTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerGameobject = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
        isAlive = true;
    }

    public bool isInFront()
    {
        Vector3 directionOfPlayer = transform.position - playerGameobject.transform.position;
        float angle = Vector3.Angle(transform.forward, directionOfPlayer);

        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            Debug.DrawLine(this.transform.position, playerGameobject.transform.position, Color.red);
            return true;
        }
        return false;
    }

    public bool isInSight()
    {
        RaycastHit hit;
        Vector3 directionOfPlayer = playerGameobject.transform.position - transform.position;
        if (Physics.Raycast(transform.position, directionOfPlayer, out hit, viewFieldDistance))
        {
            if (hit.transform.tag == "Player")
            {
                Debug.DrawLine(transform.position, playerGameobject.transform.position, Color.green);
                return true;
            }
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
    public void BattleModeOf()
    {
        imFighting = false;
        playerGameobject.GetComponent<BattleMode>().listOfEnemies.Remove(transform);
        timerOfSight = 0;
    }
    public void IsDead()
    {
        playerGameobject.GetComponent<BattleMode>().listOfEnemies.Remove(transform);
        isAlive = false;
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
    public void ReturnToStartPosition()
    {
        agent.SetDestination(startPosition);
        agent.isStopped = true;
    }
}
