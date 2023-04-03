using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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
    public List<GameObject> megaSphere;
    public List<bool> isLit;
    public List<bool> MegaSpheresLit;

    public Camera thisZoomCamera;
    public Material litMaterial;
    public Material darkMaterial;

    //if megasphere is not lit, on move it shuts down sphere that is closest to it
    // bool - wasLit, if not - shut down one
    //limit steps - as lose conditions?
    private void Awake()
    {
        pipeRotation_Center = UnityEditor.TransformUtils.GetInspectorRotation(pipe_Center.transform).y;
        pipeRotation_Middle = UnityEditor.TransformUtils.GetInspectorRotation(pipe_Middle.transform).y;
        pipeRotation_Outer = UnityEditor.TransformUtils.GetInspectorRotation(pipe_Outer.transform).y;
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
                    RotateDial_Center(); //quaternion is always new vector3
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
        if (checkList.Count == isLit.Count)
        { 
            StartCoroutine(GameWon());
            print("YOU WON!");
        }
    }

    IEnumerator GameWon()
    {
        print("you need to add MegaSpheres into win conditions");
        float timer = 0;
        while(timer < 10)
        {
            timer += Time.deltaTime;
            foreach(GameObject sphere in spheres)
            {
                sphere.transform.position = new Vector3(
                    Mathf.Lerp(sphere.transform.position.x, 0, 10),
                    Mathf.Lerp(sphere.transform.position.y, 0, 10),
                    Mathf.Lerp(sphere.transform.position.z, 0, 10));
            }
            foreach (GameObject sphere in megaSphere)
            {
                sphere.transform.position = new Vector3(
                    Mathf.Lerp(sphere.transform.position.x, 0, 10),
                    Mathf.Lerp(sphere.transform.position.y, 0, 10),
                    Mathf.Lerp(sphere.transform.position.z, 0, 10));
            }
            break;
        }
        yield return null;
    }
}
