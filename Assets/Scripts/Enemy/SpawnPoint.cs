using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPF;
    //bool if was killed
    //save/load the bool
    //onstantiate if wasnt killed
    //for Astral - create counter script
    private void Start()
    {
        Instantiate(enemyPF, transform);
    }
}
