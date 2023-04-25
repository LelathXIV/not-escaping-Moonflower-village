using System.Collections;
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
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerGameobject = GameObject.FindGameObjectWithTag("Player");
        isAlive = true;
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        StartPosition = transform.position;
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
