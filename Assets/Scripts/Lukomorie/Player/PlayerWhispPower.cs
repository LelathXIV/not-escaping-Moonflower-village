using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class PlayerWhispPower : MonoBehaviour
{
    public GameObject whispPrefab;
    public GameObject whispParent;
    public Button callWhispButton;
    public List<GameObject> whisps;
    public float whispDamagePower;
    public int maxWhispsCount;
    public float CoolDownTime;

    private void Awake()
    {
        callWhispButton.onClick.AddListener(SummonWhisp);
    }

    private void Update()
    {
        whispParent.transform.Rotate(Vector3.up, Time.deltaTime * 50, Space.Self);

      //  whispParent.transform.childCount
    }

    void SummonWhisp()
    {
        var newWhisp = Instantiate(whispPrefab, whispParent.transform);
        newWhisp.GetComponentInChildren<WhispPowerBeh>().whispDamagePower = whispDamagePower;
        AlignSpheres();
    }

    void AlignSpheres()
    {
        for (int i = 0; i < whisps.Count; i++)
        {
            //(360 / whisps.Count) * i;
        }
    }
}
