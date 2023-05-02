using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    public float enemyCurrentHealth;
    public List<GameObject> rewards;
    private void Update()
    {
        if (enemyCurrentHealth <= 0 && GetComponent<Enemy>().isAlive)
        {
            GetComponent<Enemy>().IsDead();
            Death();
        }
    }

    void Death()
    {
        gameObject.AddComponent<DeathAnimation>().DeathAnimationStarter();
        DropReward();
    }

    void DropReward()
    {
        for (int i = 0; i < rewards.Count; i++)
        {
            if (rewards[i] != null)
            {
                Instantiate(rewards[i], transform.position, new Quaternion());
            }
        }
    }
}
