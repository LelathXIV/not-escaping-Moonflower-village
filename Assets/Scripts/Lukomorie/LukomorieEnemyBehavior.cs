using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class LukomorieEnemyBehavior : MonoBehaviour
{
    LayerMask layerMask;
    public GameObject player;
    public GameObject magicSpherePrefab;
    public float maxHealth;
    public float currentHealth;
    public float speed;
    public float attackRange;
    public float attackDamageValue;
    public float magicAttackMultiplyer;
    public bool attack1CD;
    public bool magicAttackDC;
    public bool attacking;
    public bool isHit;
    Animator animatior;
    public float raycastOffsetVector = 2.5f;
    public float detectionDistance = 50f;
    public GameObject healingOrbPf;
    public Material originalMat;
    public Material getHitMat;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animatior = GetComponent<Animator>();
        layerMask = 8 | 3;
        currentHealth = maxHealth;
        originalMat = GetComponent<MeshRenderer>().material;
    }
    private void Update()
    {
        Pathfinding();
        var distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > attackRange - 1 && !attack1CD && !isHit)
        {
            Move();
            Attack(distance);
        }
        if(currentHealth <= 0)
        {
            Instantiate(healingOrbPf, transform.position, new Quaternion());
            Destroy(gameObject);
        }
    }
    void Pathfinding() //no tavigate in SPACE
    {
        Vector3 rayCastOffset = Vector3.zero;
        Vector3 left = transform.position - transform.right * raycastOffsetVector;
        Vector3 right = transform.position + transform.right * raycastOffsetVector;
        Vector3 up = transform.position + transform.up * raycastOffsetVector;
        Vector3 down = transform.position - transform.up * raycastOffsetVector;

        RaycastHit hit;

        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance, layerMask))
        {
            if(hit.transform.tag == "enemy" || hit.transform.tag == "obstacle")
            rayCastOffset += Vector3.right;
        }
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance, layerMask))
        {
            if (hit.transform.tag == "enemy" || hit.transform.tag == "obstacle")
                rayCastOffset -= Vector3.left;
        }

        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance, layerMask))
        {
            if (hit.transform.tag == "enemy" || hit.transform.tag == "obstacle")
                rayCastOffset -= Vector3.up;
        }
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance, layerMask))
        {
            if (hit.transform.tag == "enemy" || hit.transform.tag == "obstacle")
                rayCastOffset += Vector3.up;
        }

        if (rayCastOffset != Vector3.zero)
        {
            transform.Rotate(rayCastOffset * 5f * Time.deltaTime); //some value to define obj rotation speed
        }
        else Turn(); //transform.LookAt(player.transform);
    }

    public void GetHit(float incomingDamage)
    {
        currentHealth -= incomingDamage;
        print(currentHealth);
        StartCoroutine(GetHitCoroutine());
    }

    IEnumerator GetHitCoroutine()
    {
        isHit = true;
        GetComponent<MeshRenderer>().material = getHitMat;
        yield return new WaitForSeconds(0.3f);
        GetComponent<MeshRenderer>().material = originalMat;
        isHit = false;
    }
    void Turn()
    {
        Vector3 pos = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
    }
    void Move()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    void Attack(float distance)
    {
        if(distance <= attackRange && !attack1CD)
        {
            StartCoroutine(AttackMele());
        }
        if (distance <= attackRange && !magicAttackDC)
        {
            StartCoroutine(MagicAttack());
        }
    }
    IEnumerator AttackMele()
    {
        attack1CD = true;
        attacking = true;
        float distanceNew = Vector3.Distance(transform.position, player.transform.position);
        animatior.SetTrigger("attack1");
        if (distanceNew <= attackRange)
        {
            player.GetComponent<LukomoriePlayerHealth>().GetDamage(attackDamageValue);
        }
        yield return new WaitForSeconds(3);
        attack1CD = false;
        attacking = false;
        StopCoroutine(AttackMele());
    }

    IEnumerator MagicAttack()
    {
        magicAttackDC = true;
        attacking= true;
        var sphere = Instantiate(magicSpherePrefab, transform.position, new Quaternion());
        sphere.GetComponent<LukomorieProjectileBeh>().damageValue = attackDamageValue * magicAttackMultiplyer;
        var startScale = sphere.transform.localScale;
        float scaleModifier = 1;
        float startValue = scaleModifier;
        var endValue = 10;
        float elapsedTime = 0;
        while (elapsedTime < 3)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, elapsedTime / 3);
            sphere.transform.localScale = startScale * scaleModifier;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        sphere.transform.localScale = startScale;
        yield return new WaitForSeconds(5);
        magicAttackDC = false;
        attacking = false;
    }
}
