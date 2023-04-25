using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float DamageValue;
    public void OnTriggerEnter(Collider other)
    {
        //�������� ���� ���� ������ � ��������� �������� ����
        if (other.gameObject.tag == "enemy")
        {
            other.transform.GetComponent<EnemyHealth>().enemyCurrentHealth -= DamageValue;
            other.GetComponent<Enemy>().BattleModeOn();
            Destroy(gameObject);
        }
    }
}
