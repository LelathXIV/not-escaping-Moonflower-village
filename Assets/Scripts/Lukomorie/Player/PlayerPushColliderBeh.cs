using System.Collections;
using UnityEngine;

public class PlayerPushColliderBeh : MonoBehaviour
{
    public float duration = 5;
    Vector3 targetPosition;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "enemy")
        {
            print("!!");

            GetPosition(c.transform);
        }
    }

    void GetPosition(Transform enemy)
    {
        targetPosition = - Vector3.forward;
        StartCoroutine(LerpPosition(enemy.transform));
    }
    
    IEnumerator LerpPosition(Transform enemy)
    {
        float time = 0;
        Vector3 startPosition = enemy.transform.position;
        while (time < duration)
        {
            enemy.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        enemy.transform.position = targetPosition;
        StopCoroutine(LerpPosition(enemy));
    }
}
