using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        //�������� ���� ���� ������ � ��������� �������� ����
        if (other.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
            other.GetComponent<Enemy>().BattleModeOn();
        }
    }
}
