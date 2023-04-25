using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Zenject.Asteroids;

public class PlayerStats : MonoBehaviour
{
    public float playerMaxHealth;
    public float playerCurrentHealth;
    public float aimingSpeedBoost; //from 0.1 to 1. The lower, the faster. Summary from game it must be 0.5
    public float playerMeleDamage;
    public float playerGunDamage;
    public bool died;

    private void Start()
    {
        var playerSaves = SaveGameManager.CurrentSaveData;
        if (playerSaves.gameStarted)
        {
            playerMaxHealth = playerSaves.MAXplayerHP;
            playerCurrentHealth = playerSaves.currentPlayerHP;
            print("loading player health");
        }
        else InitialStats();
    }



    private void Update()
    {
        if (playerCurrentHealth <= 0)
        {
            // Debug.Log("Im dead!!!! =(((((((");
            //miracle event
            //teleport to Lukomorie
            if (!died)
            {
                died = true;
                StartCoroutine(Death());
                GetComponent<PlayerAnimations>().Death();
            }
        }
        SavePlayerStats();
    }

    public void InitialStats()
    {
        playerMaxHealth = 100; //add HP boosters here
        playerCurrentHealth = playerMaxHealth;
        aimingSpeedBoost = 1; //add boosters here
    }

    void SavePlayerStats()
    {
        SaveGameManager.CurrentSaveData.currentPlayerHP = playerCurrentHealth;
        SaveGameManager.CurrentSaveData.currentPlayerHP = playerCurrentHealth;
    }
    public void GetHit(float damage)
    {
        GetComponent<PlayerAnimations>().GetHit();
        playerCurrentHealth -= damage;
        print(damage);
        print(playerCurrentHealth);
        GetComponent<PlayerShootingSystem>().StopAiming();
    }

    IEnumerator Death()
    {
        print("start dying");
        yield return new WaitForSeconds(1);
        StartCoroutine(DeathAnimation());
        GameObject.FindGameObjectWithTag("canvas").gameObject.SetActive(false);
        GetComponent<FadeInFadeOut>().fadeSpeed = 3;
        GetComponent<FadeInFadeOut>().StartFadeIn();
        yield return new WaitForSeconds(3);
        //save player scene here
        var playerSaves = GetComponent<PlayerSaveEvents>();
        playerSaves.GetSceneNumber();
        playerSaves.isInLukomorie = true;
        playerSaves.SavePlayerScene();
        SceneManager.LoadScene("Lukomorie");
    }

    IEnumerator DeathAnimation()
    {
        var duration = 3;
        var deathFire = Resources.Load("deathCircle_PF") as GameObject;
        var deathEffect = Instantiate(deathFire, transform.position, new Quaternion());
        Vector3 startScale = deathEffect.transform.localScale;
        float scaleModifier = 1;
        float startValue = scaleModifier;

        var endValue = 0;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            deathEffect.transform.localScale = startScale * scaleModifier;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}



