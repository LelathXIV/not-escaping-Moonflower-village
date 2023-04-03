using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLitting : MonoBehaviour
{
    public Material litMat;
    public bool isLit;
    public GameObject lockMg;
    public ScionMG scionMG;

    private void Start()
    {
        scionMG = lockMg.GetComponent<ScionMG>();
        for (int i = 0; i < scionMG.spheres.Count; i++)
        {
            if (scionMG.spheres[i] == this.gameObject)
            {
                isLit = scionMG.isLit[i];
            }
        }
        if (isLit)
            GetComponent<MeshRenderer>().material = litMat;
    }

    public void OnTriggerStay (Collider other)
    {
        if(other.tag == "miniGame")
        {
            if (isLit)
            {
                print(other.transform.tag);
                other.GetComponent<MeshRenderer>().material = litMat;
                other.GetComponent<SphereLitting>().isLit = true;
                PassLitBool();
                other.GetComponent<SphereLitting>().PassLitBool();
            }
        }
    }

    public void PassLitBool()
    {
        for (int i = 0; i < scionMG.spheres.Count; i++)
        {
            if (scionMG.spheres[i] == this.gameObject)
            {
                scionMG.isLit[i] = isLit;
            }
        }
        scionMG.CheckNearSpheres();
    }
}
