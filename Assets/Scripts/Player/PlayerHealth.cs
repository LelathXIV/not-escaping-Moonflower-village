using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerMaxHealth;
    public float playerCurrentHealth;

    private void Awake()
    {
        playerMaxHealth = 100;
        playerCurrentHealth = 100;
    }

    private void Update()
    {
        if(playerCurrentHealth >=100 )
        {
            playerCurrentHealth = playerMaxHealth;
        }
        if(playerCurrentHealth <= 0)
        {
            Debug.Log("Im dead!!!! =(((((((");
        }
    }
}
