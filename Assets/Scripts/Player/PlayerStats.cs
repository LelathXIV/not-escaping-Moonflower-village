using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float playerMaxHealth;
    public float playerCurrentHealth;
    public float aimingSpeedBoost; //from 0.1 to 1. The lower, the faster. Summary from game it must be 0.5

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
