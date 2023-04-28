using System.Collections;
using UnityEngine;

public class GolemProjectileBeh : MonoBehaviour
{
    GameObject player;
    Vector3 direction;
    public float ProjectileSpeed;
    public float projectileDamage;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = new Vector3( player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
        StartCoroutine(LerpPosition(direction, ProjectileSpeed));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().GetHit(projectileDamage);
        }
        if(other.CompareTag("obstacle"))
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }
}
