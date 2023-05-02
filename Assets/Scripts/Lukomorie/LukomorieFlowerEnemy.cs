using System.Collections;
using UnityEngine;

public class LukomorieFlowerEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePF;
    public bool attackCD;
    public float attackDistance;
    public float attackCooldown;
    public float projectileSpeed;
    public float projectileDamage;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Turn();
        var distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= attackDistance && !attackCD)
        {
            StartCoroutine(Attack());
        }
    }

    void Turn()
    {
        Quaternion _lookRotation = Quaternion.LookRotation((player.transform.position - transform.position).normalized);
        //over time
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime);
    }

    IEnumerator Attack()
    {
        attackCD = true;

        GameObject projectile = Instantiate(projectilePF, transform.transform.position, Quaternion.LookRotation(player.transform.position));
        projectile.transform.localScale = new Vector3(4, 4, 4);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.isKinematic = false;
        projectileRb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);

        projectile.GetComponent<LukomorieProjectileBeh>().damageValue = projectileDamage;

        yield return new WaitForSeconds(attackCooldown);
        attackCD = false;
    }
}
