using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScionMG : MonoBehaviour
{
    public Transform pipe_Center;
    public Transform pipe_Middle;
    public Transform pipe_Outer;

    public float pipeRotation_Center;
    public float pipeRotation_Middle;
    public float pipeRotation_Outer;

    public List<GameObject> spheres;
    public List<bool> isLit;

    public Camera thisZoomCamera;
    public Material litMaterial;
    public Material darkMaterial;

    private void Awake()
    {
        pipeRotation_Center = UnityEditor.TransformUtils.GetInspectorRotation(pipe_Center.transform).y;
        pipeRotation_Middle = UnityEditor.TransformUtils.GetInspectorRotation(pipe_Middle.transform).y;
        pipeRotation_Outer = UnityEditor.TransformUtils.GetInspectorRotation(pipe_Outer.transform).y;
    }

    private void Start()
    {
        for (int x = 0; x < isLit.Count; x++)
        {
            if (isLit[x])
            {
                spheres[x].GetComponent<MeshRenderer>().material = litMaterial;
                spheres[x].GetComponent<SphereLitting>().isLit = true;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = thisZoomCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("zoomCamera")))
            {
                if (hit.transform == pipe_Center) 
                {
                    RotateDial_Center(); //quaternion is always new vector3 - !!!!!!!
                }
                if (hit.transform == pipe_Middle) 
                {
                    RotateDial_Middle();
                }
                if (hit.transform == pipe_Outer) 
                {
                    RotateDial_Outer();
                }
            }
        }
    }

    void RotateDial_Center()
    {
        pipeRotation_Center += 30;
        pipe_Center.transform.localRotation = Quaternion.AngleAxis(pipeRotation_Center, Vector3.up);
        if(pipeRotation_Center >= 360)
        {
            pipeRotation_Center = 0;
        }
    }

    void RotateDial_Middle()
    {
        pipeRotation_Middle += 30;
        pipe_Middle.transform.localRotation = Quaternion.AngleAxis(pipeRotation_Middle, Vector3.up);
        if (pipeRotation_Middle >= 360)
        {
            pipeRotation_Middle = 0;
        }
    }

    void RotateDial_Outer()
    {
        pipeRotation_Outer += 30;
        pipe_Outer.transform.localRotation = Quaternion.AngleAxis(pipeRotation_Outer, Vector3.up);
        if (pipeRotation_Outer >= 360)
        {
            pipeRotation_Outer = 0;
        }
    }
    public void CheckNearSpheres()
    {
        var checkList = new List<bool>();
        for (int i = 0; i < isLit.Count; i++)
        {
            if (isLit[i] == true)
                checkList.Add(isLit[i]);
        }
        if(checkList.Count == 0)
        {
            print("lost");
            //resetMG
        }
        if (checkList.Count == isLit.Count)
        { 
          //  StartCoroutine(GameWon());
            print("YOU WON!");
            //runANimation
        }
    }
}
