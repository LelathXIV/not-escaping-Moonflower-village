using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LukomorieEnemyGeneranor : MonoBehaviour
{
    public float duration;
    public bool scandCD;
    public Vector3 startScale;
    public float finalScale;

    private void Awake()
    {
        startScale = transform.localScale;
    }

    private void Update()
    {
        if (!scandCD) StartCoroutine(ScanArea());
    }

    IEnumerator ScanArea()
    {
        scandCD = true;
        float scaleModifier = 1;
        float startValue = scaleModifier;

        var endValue = finalScale;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            transform.localScale = startScale * scaleModifier;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localScale = startScale;
        yield return new WaitForSeconds(5);
        scandCD = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            transform.parent.GetComponent<LukomorieEnemySpawn>().CreateEnemies();
        }
    }
}
