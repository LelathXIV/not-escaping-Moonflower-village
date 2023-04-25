using System.Collections;
using UnityEngine;

public class WhispPowerBeh : MonoBehaviour
{
    public float whispDamagePower;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            StartCoroutine(HitEnemy(other.transform));
            print("!!");
        }
    }

    IEnumerator HitEnemy(Transform other)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, other.position, 50 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        other.GetComponent<LukomorieEnemyBehavior>().GetHit(whispDamagePower);
        StopCoroutine(HitEnemy(other));
        Destroy(transform.parent.gameObject);
    }
}
