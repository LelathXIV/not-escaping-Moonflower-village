using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMode : MonoBehaviour
{
    public bool isInBattle;
    public float damping;
    public Transform enemy;
    public List<Transform> listOfEnemies;


    //выключить весь UI кроме боевого (слот оружия и этот слот) если в бою

    private void Update()
    {
        if(listOfEnemies.Count == 0)
        {
            isInBattle = false;
        }
        else
        {
            isInBattle = true;
            FindNearestEnemy();
        }
    }

    void FindNearestEnemy()
    {
        var smallestDistance = Mathf.Infinity;
        foreach (Transform _enemy in listOfEnemies)
        {
            var distance = Vector3.Distance(transform.position, _enemy.position);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                enemy = _enemy;
            }
        }
    }

    public void LookAtTargetEnemy()
    {
        var target = enemy.transform;
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
