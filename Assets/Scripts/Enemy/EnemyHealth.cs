using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    public float enemyCurrentHealth;

    private void Awake()
    {

    }
    private void Update()
    {
        if(enemyCurrentHealth <= 0)
        {
            gameObject.GetComponent<EnemyBehaviorTest>().StopAllCoroutines();
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
