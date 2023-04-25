using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LukomoriePlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject totemSpawner;
    public Transform enemyGenerators;
    public GameObject healthParent;
    public GameObject healthBar;
    public bool dead;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0 && !dead)
        {
            StartCoroutine(Restart());
        }
        healthBar.transform.localScale = new Vector3
            (currentHealth / maxHealth,
            healthBar.transform.localScale.y,
            healthBar.transform.localScale.z);
        healthParent.transform.LookAt(Camera.main.transform);
    }

    public void GetDamage(float incomingDamage)
    {
        currentHealth -= incomingDamage;
    }

    IEnumerator Restart()
    {
        dead = true;
        totemSpawner.GetComponent<FadeInFadeOut>().StartFadeIn();
        totemSpawner.GetComponent<TotemsSpawner>().ConfigSpawn();
        foreach(Transform child in enemyGenerators)
        {
            child.GetComponent<LukomorieEnemySpawn>().DestroyAllEnemies();
        }
        currentHealth = maxHealth;
        healthBar.transform.localScale = new Vector3(1, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        dead = false;
        yield return null;
    }
}
