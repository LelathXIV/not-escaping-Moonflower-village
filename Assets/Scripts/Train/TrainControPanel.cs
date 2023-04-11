using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainControPanel : MonoBehaviour
{
    public Camera thisZoomCamera;
    public Material inactiveMat;
    public Material activeMat;
    public Material currentMat;
    public int currentSceneNumber;

    public List<Material> locationMaterials;
    public List<GameObject> buttons;
    public List<GameObject> actionTriggers;
    public List<bool> CurrentLocationCheck;
    public List<bool> isActiveDestination;
    public List<bool> newDestination;
    public GameObject display;
    public GameObject bigArrow;

    private void Start()
    {
        LoadSaveGame();
        StartCoroutine(CheckButtonsAtStart());
        CheckButtonStatus();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = thisZoomCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10, 1 << LayerMask.NameToLayer("zoomCamera")))
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (hit.transform.gameObject == buttons[i])
                    {
                        CheckDestination(i);
                    }
                    if(hit.transform.gameObject == bigArrow)
                    {
                        for (int x = 0; x < newDestination.Count; x++)
                        {
                            if (newDestination[x] == true)
                            {
                                GetComponent<SceneSwitcher>().nextSceneNumber = x;
                                ChangeLocation();
                            }
                        }
                        //load scene connected to new destination [i] (the only TRUE dest)
                    }
                }
            }
        }
        CheckButtonStatus();
    }

    void ChangeLocation()
    {
        if (!GetComponent<OnArriving>().arriving)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<FadeInFadeOut>().StartFadeIn();
            GetComponent<SceneSwitcher>().LoadNextScene();
        }
        else
        {
            GetComponent<OnArriving>().arriving = false;
        }
    }

    void LoadSaveGame()
    {
        var saveGame = SaveGameManager.CurrentSaveData._trainSaveData;
        if (saveGame != null)
        {
            var trainSaveData = SaveGameManager.CurrentSaveData._trainSaveData;
            isActiveDestination = trainSaveData.isActiveDestination;
            for (int i = 0; i < isActiveDestination.Count; i++)
            {
                if (isActiveDestination[i] && actionTriggers[i] != null)
                {
                    actionTriggers[i].GetComponent<QuestColliders>().RunAnimation();
                }
            }
        }
    }
    IEnumerator CheckButtonsAtStart()
    {
        yield return new WaitForSeconds(3);
        CheckButtonStatus();
        CheckCurrentLocation();
    }

    public void CheckButtonStatus()
    {
        for (int i = 0; i < actionTriggers.Count; i++)
        {
            if (actionTriggers[i] == null)
            {
                buttons[i].GetComponent<MeshRenderer>().material = activeMat;
                isActiveDestination[i] = true;
            }
        }
    }

    public void CheckCurrentLocation()
    {
        for (int i = 0; i < CurrentLocationCheck.Count; i++)
        {
            if (CurrentLocationCheck[i])
            {
                buttons[i].GetComponent<MeshRenderer>().material = currentMat;
                display.GetComponent<MeshRenderer>().material = locationMaterials[i];
            }
            SaveTrainData();
        }
    }

    public void CheckDestination(int i)
    {
        if(isActiveDestination[i] && !CurrentLocationCheck[i])
        {
            display.GetComponent<MeshRenderer>().material = locationMaterials[i];
            bigArrow.GetComponent<MeshRenderer>().material = activeMat;
            for (int x = 0; x < newDestination.Count; x++)
            {
                newDestination[i] = false;
            }
            newDestination[i] = true;
        }
    }

    public void SaveTrainData()
    {
        var thisTrainSaveData = new TrainSaveData();
        thisTrainSaveData.isActiveDestination = isActiveDestination;
        SaveGameManager.CurrentSaveData._trainSaveData = thisTrainSaveData;
        SaveGameManager.SaveGame();
    }
}

[System.Serializable]
public class TrainSaveData
{
    public List<bool> isActiveDestination;
}
