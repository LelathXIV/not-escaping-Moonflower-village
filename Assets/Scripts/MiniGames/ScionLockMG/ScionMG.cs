using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScionMG : MonoBehaviour
{
    public Transform pipe_Center;
    public Transform pipe_Middle;
    public Transform pipe_Outer;

    public List<GameObject> spheres;
    public List<bool> isLit;

    public Camera thisZoomCamera;
    public Material litMaterial;
    public Material darkMaterial;
    public bool scionMGFinished;
    public bool isReseting;
    public List<GameObject> rewards;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        var mgSaveData = SaveGameManager.CurrentSaveData._scionMgSaveData;
        for (int i = 0; i < mgSaveData.Count; i++)
        {
            print("looking for saves");
            if (mgSaveData[i].position == transform.position && mgSaveData[i].isFinished == true)
            {
                scionMGFinished = true;
                print("scion MG finished");
            }
        }
        if(scionMGFinished)
        {
            foreach(GameObject spere in spheres)
            {
                spere.GetComponent<MeshRenderer>().material = litMaterial;
                spere.GetComponent<SphereLitting>().isLit = true;
                animator.SetTrigger("Active");
                StopAllCoroutines();
            }
        }
        else
        {
            for (int x = 0; x < isLit.Count; x++)
            {
                if (isLit[x])
                {
                    spheres[x].GetComponent<MeshRenderer>().material = litMaterial;
                    spheres[x].GetComponent<SphereLitting>().isLit = true;
                }
            }
          //  GetInspectorRotations();
        }
    }

    private void Update()
    {
        if(!scionMGFinished && !isReseting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = thisZoomCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("zoomCamera")))
                {
                    if(hit.transform.tag == "openable")
                    {
                        StartCoroutine(RotateDial(hit));
                        print(hit.transform.name);
                    }
                }
            }
            CheckNearSpheres();
        }
    }

    IEnumerator RotateDial(RaycastHit hit)
    {
        float t = 0;
        while (t < 1 )
        {
            t += Time.deltaTime;

            Vector3 direction = new Vector3(hit.transform.localRotation.eulerAngles.x, Mathf.Round(hit.transform.localRotation.eulerAngles.y + 30), hit.transform.localRotation.eulerAngles.z);
            Quaternion targetRotation = Quaternion.Euler(direction);
            hit.transform.localRotation = Quaternion.Lerp(hit.transform.localRotation, targetRotation, Time.deltaTime * 1);
            yield return null;
        }
    }

    public void CheckNearSpheres()
    {
        var mgStatus = new List<bool>();
        for (int i = 0; i < spheres.Count; i++)
        {
            isLit[i] = spheres[i].GetComponent<SphereLitting>().isLit;
            if(isLit[i])
                 mgStatus.Add(isLit[i]);
        }
        if (mgStatus.Count == spheres.Count)
        {
            print("MG WON!");
            scionMGFinished = true;
            SaveMGStatus();
            animator.SetTrigger("Active");
        }
        if (mgStatus.Count == 0)
        {
            print("resetMG");
            isReseting = true;
            StartCoroutine(ResetMG());
        }
    }

    public void ShowRewards()
    {
        for (int i = 0; i < rewards.Count; i++)
        {
            if (rewards[i] == null)
            {
                rewards.Remove(rewards[i]);
            }
            else rewards[i].SetActive(true);
        }
        StopAllCoroutines();
    }

    IEnumerator ResetMG()
    {
        foreach (GameObject sphere in spheres)
        {
            sphere.GetComponent<MeshRenderer>().material = darkMaterial;
            sphere.GetComponent<SphereLitting>().isLit = false;
        }
        for (int i = 0; i < isLit.Count; i++)
        {
            isLit[i] = false;
        }

        var center = Random.Range(0, 12) * 30;
        var middle = Random.Range(0, 12) * 30;
        var outer = Random.Range(0, 12) * 30;
        float t = 0;
        while(t < 0.5 && isReseting)
        {
            t += Time.deltaTime;
            pipe_Center.transform.localRotation = Quaternion.Lerp(pipe_Center.transform.localRotation, Quaternion.AngleAxis(center, Vector3.up), t);
            pipe_Middle.transform.localRotation = Quaternion.Lerp(pipe_Middle.transform.localRotation, Quaternion.AngleAxis(middle, Vector3.up), t);
            pipe_Outer.transform.localRotation = Quaternion.Lerp(pipe_Outer.transform.localRotation, Quaternion.AngleAxis(outer, Vector3.up), t);
            yield return null;
        }

        var randomSphere = Random.Range(3, 12);
        spheres[randomSphere].GetComponent<SphereLitting>().isLit = true;
        spheres[randomSphere].GetComponent<MeshRenderer>().material = litMaterial;
        isLit[randomSphere] = true;
        isReseting = false;
    }


    void SaveMGStatus()
    {
        var saveExists = false;
        for (int i = 0; i < SaveGameManager.CurrentSaveData._scionMgSaveData.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._scionMgSaveData[i].position == transform.position)
            {
                saveExists = true;
            }
        }
        if (!saveExists)
        {
            var thisMGSaveData = new ScionMGSaveData();
            thisMGSaveData.position = transform.position;
            thisMGSaveData.isFinished = scionMGFinished;
            SaveGameManager.CurrentSaveData._scionMgSaveData.Add(thisMGSaveData);
            SaveGameManager.SaveGame();
        }
    }
}
[System.Serializable]
public class ScionMGSaveData
{
    public Vector3 position;
    public bool isFinished;
}
