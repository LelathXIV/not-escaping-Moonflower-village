using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class HealingOrbBehavior : MonoBehaviour
{
    public Transform player;
    public Transform beautyChild;
    public float range;
    public float movespeed;
    public float max;
    public float min;
    public float healingPower;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healingPower = Random.Range(min, max);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime, Space.Self);
        beautyChild.transform.Rotate(Vector3.up * 100 * Time.deltaTime, Space.Self);
        var distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= range)
        {
            Move();
        }
    }

    private void Move()
    {
        // Move our position a step closer to the target.
        var step = movespeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, player.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            player.position *= -1.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<LukomoriePlayerHealth>().currentHealth += healingPower;
            Destroy(gameObject);
        }
    }
}
