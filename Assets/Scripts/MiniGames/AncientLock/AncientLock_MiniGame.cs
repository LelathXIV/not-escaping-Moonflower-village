using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class AncientLock_MiniGame : MonoBehaviour
{
    public bool mgStarted;
    public bool mgFinished;
    public Camera thisZoomCamera;
    public GameObject turnRight;
    public GameObject turnLeft;
    public GameObject lockObject;
    public GameObject keyObject;
    public GameObject keyCenter;
    public GameObject chestTop;
    public Material correctMaterial;
    public Material resetMaterial;

    public List<GameObject> progressIndicators;
    public List<bool> mgSequence;
    public List<bool> playerSequence;
    public List<GameObject> rewards;

    public float rotatingSpeed;
    public bool isRotatingRight;
    public bool isRotatingLeft;

    private float z;
    Quaternion startRotation;

    private void Start()
    {
        foreach (GameObject reward in rewards)
        {
            reward.SetActive(false);
        }
        for (int i = 0; i < SaveGameManager.CurrentSaveData._ancientChestMGsSaveDatas.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._ancientChestMGsSaveDatas[i].chestCoordinates == transform.position)
            {
                if (SaveGameManager.CurrentSaveData._ancientChestMGsSaveDatas[i].MGstarted == true)
                {
                    foreach (GameObject indicator in progressIndicators)
                    {
                        indicator.GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                    mgStarted = true;
                    GetComponent<Animator>().SetTrigger("Active");
                }
            }
                
            if (SaveGameManager.CurrentSaveData._ancientChestMGsSaveDatas[i].MGfinished == true)
            {
                foreach (GameObject reward in rewards)
                {
                    reward.SetActive(true);
                }
                chestTop.SetActive(false);
            }//load finished state of chest
        }
        startRotation = lockObject.transform.localRotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = thisZoomCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10, 1 << LayerMask.NameToLayer("zoomCamera")) && !isRotatingRight && !isRotatingLeft) 
            {
                if (hit.transform == turnRight.transform) //если попал по правой кнопке или попал по правой кнопке - делаем механику
                {
                    isRotatingRight = true;
                }
                if (hit.transform == turnLeft.transform) //если попал по левой кнопке или попал по правой кнопке - делаем механику
                {
                    isRotatingLeft = true;
                }
            }
        }

        if(isRotatingRight)
        {
            z += Time.deltaTime * rotatingSpeed;
            if (z > 360f)
            {
                z = 0.0f;
                isRotatingRight = false;
                StartCoroutine(OnClick_Right());
            }
            lockObject.transform.localRotation = startRotation * Quaternion.AngleAxis(z, Vector3.forward);
            //next time you have "center pivot problems" - check hyrarchy vector organisation
        }

        if (isRotatingLeft)
        {
            z -= Time.deltaTime * rotatingSpeed;
            if (z < -360f)
            {
                z = 0.0f;
                isRotatingLeft = false;
                StartCoroutine(OnClick_Left());
            }
            lockObject.transform.localRotation = startRotation * Quaternion.AngleAxis(z, Vector3.forward);
        }

    }
    // if turned right = true, if turned left = false
    IEnumerator OnClick_Right()
    {
        playerSequence.Add(true);
        CheckSequence();
        yield return null;
    }
    IEnumerator OnClick_Left()
    {
        playerSequence.Add(false);
        CheckSequence();
        yield return null;
    }
    void CheckSequence()
    {
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (mgSequence[i] == playerSequence[i])
            {
                progressIndicators[i].GetComponent<MeshRenderer>().material = correctMaterial;
            }
            else
            {
                ResetMG();
            }
        }
        CheckMatch();
        if(CheckMatch())
        {
            Debug.Log("you won!");
            GameWon();
        }
    }

    void GameWon()
    {
        keyCenter.GetComponent<MeshRenderer>().material = correctMaterial;
        mgFinished = true;
        SaveMG();
        foreach (GameObject reward in rewards)
        {
            reward.SetActive(true);
        }
        chestTop.SetActive(false);
    }

    bool CheckMatch()
    {
        if (playerSequence.Count != mgSequence.Count)
            return false;
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != mgSequence[i])
                return false;
        }
        return true;
    }

    void ResetMG()
    {
        playerSequence.Clear();
        foreach(GameObject indicator in progressIndicators)
        {
            indicator.GetComponent<MeshRenderer>().material = resetMaterial;
        }
    }

    public void StartMG()
    {
        print("starting MG");

        foreach (GameObject indicator in progressIndicators)
        {
            indicator.GetComponent<MeshRenderer>().material.color = Color.black;
        }
        mgStarted = true;
        keyObject.SetActive(true);
        SaveMG();
    }

    void SaveMG()
    {
        var saveExists = false;
        for (int i = 0; i < SaveGameManager.CurrentSaveData._ancientChestMGsSaveDatas.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._ancientChestMGsSaveDatas[i].chestCoordinates == transform.position)
            {
                saveExists = true;
            }
        }
        if(!saveExists)
        {
            var thisMGSaveData = new AncientChestMGsSaveData();
            thisMGSaveData.chestCoordinates = transform.position;
            thisMGSaveData.MGstarted = mgStarted;
            thisMGSaveData.MGfinished = mgFinished;
            SaveGameManager.CurrentSaveData._ancientChestMGsSaveDatas.Add(thisMGSaveData);
            SaveGameManager.SaveGame();
        }
    }
}

[System.Serializable]
public class AncientChestMGsSaveData
{
    public Vector3 chestCoordinates;
    public bool MGstarted;
    public bool MGfinished;
}

