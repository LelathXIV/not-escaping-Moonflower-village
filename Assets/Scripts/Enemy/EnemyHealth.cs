using UnityEngine;
using Zenject;

public class EnemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    public float enemyCurrentHealth;
    [Inject] IPlayerWeapon playerWeapon;

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
    }

  //IEnumerator LerpScale()
  //{
  //    float scaleModifier = 1;
  //    float time = 0;
  //    float startValue = scaleModifier;
  //    var endValue = 0;
  //    var duration = deathDuration;
  //    Vector3 startScale = transform.localScale;
  //    while (time < duration)
  //    {
  //        scaleModifier = Mathf.Lerp(startValue, endValue, time / duration);
  //        transform.localScale = startScale * scaleModifier;
  //        time += Time.deltaTime;
  //        yield return null;
  //    }
  //    transform.localScale = startScale * endValue;
  //}
}
