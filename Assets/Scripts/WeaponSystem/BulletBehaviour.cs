using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        //передать сюда типа оружия и параметры здоровья моба
        if (other.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
            other.GetComponent<Enemy>().BattleModeOn();
        }
    }
}
