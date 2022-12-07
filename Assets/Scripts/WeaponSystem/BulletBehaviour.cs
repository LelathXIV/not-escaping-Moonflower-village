using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public GameObject player;
    public float playerDamageValue;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerDamageValue = player.GetComponent<PlayerShootingSystem>().damageValue;
    }

    public void OnTriggerEnter(Collider other)
    {
        //передать сюда типа оружия и параметры здоровья моба
        if(other.gameObject.tag == "enemy")
        {
            other.GetComponent<EnemyHealth>().enemyCurrentHealth -= playerDamageValue;

            print( other.name + other.GetComponent<EnemyHealth>().enemyCurrentHealth);
            Destroy(gameObject);
        }
    }
}
