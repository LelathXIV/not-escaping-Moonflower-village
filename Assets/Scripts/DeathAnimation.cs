using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    GameObject deathFire;
    GameObject deathEffect;
    float duration;

    private void Awake()
    {
        deathFire = Resources.Load("deathCircle_PF") as GameObject;
        duration = 2;
    }

    public void DeathAnimationStarter()
    {
        deathEffect = Instantiate(deathFire, transform.position, new Quaternion());
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position - (transform.up * 5);
        Vector3 startScale = transform.localScale;

        float scaleModifier = 1;
        float startValue = scaleModifier;

        var endValue = 0;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / duration));
            scaleModifier = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            transform.localScale = startScale * scaleModifier;
            deathEffect.transform.localScale = startScale * scaleModifier;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localScale = startScale * endValue;
        Destroy(deathEffect);
        Destroy(gameObject);
    }
}
