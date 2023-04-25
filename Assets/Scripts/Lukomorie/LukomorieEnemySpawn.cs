using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LukomorieEnemySpawn : MonoBehaviour
{
    public List<GameObject> listOfEnemiesPF;
    public GameObject enemyParent;
    public bool playerDetected;

    private void Start()
    {
        int enemyCount = 10;
        print("adding enemies PF");
        var enemyPF = Resources.Load("Prefabs/Lukomorie/LukomorieSphereEnemy") as GameObject;
        for (int i = 0; i < enemyCount; i++)
        {
            listOfEnemiesPF.Add(enemyPF);
        }
    }

    public void CreateEnemies()
    {
        StartCoroutine(InstantiateEnemies());
    }
    IEnumerator InstantiateEnemies()
    {
        for (int i = 0; i < listOfEnemiesPF.Count; i++)
        {
            var enemy = Instantiate(listOfEnemiesPF[i], enemyParent.transform);
            enemy.transform.position = enemyParent.transform.position;
            enemy.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            yield return new WaitForSeconds(1f);
        }
        StopCoroutine(InstantiateEnemies());
    }

    public void DestroyAllEnemies()
    {
        foreach(Transform enemy in enemyParent.transform)
        {
            enemy.GetComponent<LukomorieEnemyBehavior>().StopAllCoroutines();
            Destroy(enemy.gameObject);
            StopAllCoroutines();
        }
    }
}
