using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.AI;

public class EnemyBehaviorTest : MonoBehaviour
{
    private Transform player;
    private GameObject playerGameobject;
    private bool isRunning;
    private IEnumerator following;
    private NavMeshAgent agent;

    public float attackRange;
    public float attacDamageValue;
    public float attacDelay;
    bool isDelaying;

    private void Awake()
    {
        following = FollowPlayer();
        agent = GetComponent<NavMeshAgent>();
        playerGameobject = GameObject.FindGameObjectWithTag("Player");
    }    



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.transform;
            isRunning = true;
            StartCoroutine(following);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" )
        {
            if (isDelaying != true)
            {
                float distance = Vector3.Distance(this.transform.position, other.transform.position);
                if (distance <= attackRange)
                {
                    playerGameobject.GetComponent<PlayerHealth>().playerCurrentHealth -= attacDamageValue;
                    isDelaying = true;
                    print(playerGameobject.GetComponent<PlayerHealth>().playerCurrentHealth);
                    StartCoroutine(ShootDelay());
                }
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopCoroutine(following);
        }
    }

    IEnumerator FollowPlayer()
    {
        while(isRunning == true)
        {
           // transform.LookAt(player.transform.up);
            agent.SetDestination(player.position);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(attacDelay);
        isDelaying = false;
        print(isDelaying);
    }
   
}
