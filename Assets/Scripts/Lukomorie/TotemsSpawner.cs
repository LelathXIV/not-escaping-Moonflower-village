using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemsSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public GameObject player;
    public GameObject totemPrefab;
    public GameObject instantiatedTotem;
    public int randomExit;
    public int randomEntrance;

    private void Awake()
    {
        ConfigSpawn();
    }
    public void ConfigSpawn()
    {
        RandomInts();
    }
    void PlayerRandomLocation()
    {
        player.gameObject.SetActive(false);
        player.transform.position = spawnPoints[randomEntrance].position;
        player.gameObject.SetActive(true);
    }

    void InstantiateExitTotem()
    {
        instantiatedTotem = Instantiate(totemPrefab, spawnPoints[randomExit].position, new Quaternion());
    }

    void RandomInts()
    {
        Destroy(instantiatedTotem.gameObject);
        var list = new List<int>();   //  Declare list
        for (int n = 0; n < spawnPoints.Count; n++)    //  Populate list
        {
            list.Add(n);
        }
        int index = Random.Range(0, list.Count - 1);    //  Pick random element from the list
        randomExit = list[index]; //the number that was randomly picked
        list.RemoveAt(index);
        randomEntrance = list[Random.Range(0, list.Count - 1)];
        InstantiateExitTotem();
        PlayerRandomLocation();
        GetComponent<FadeInFadeOut>().StartFadeOut();
    }
}
