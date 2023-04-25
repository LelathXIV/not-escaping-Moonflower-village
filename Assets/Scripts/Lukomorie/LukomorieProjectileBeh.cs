using System.Collections;
using UnityEngine;
using Zenject.SpaceFighter;

public class LukomorieProjectileBeh : MonoBehaviour
{
    public float damageValue { get; set; }
    private void Update()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime, Space.Self);
    }
    private void Start()
    {
        StartCoroutine(DestroyBulletAfterTime());
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<LukomoriePlayerHealth>().GetDamage(damageValue);
        }
    }

    private IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(3.1f);
        Destroy(gameObject);
    }
}
