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
                        if (isActiveDestination[i] && !CurrentLocationCheck[i])
                        {
                            ChangeLocation(i);
                        }
                    }
                }
            }
        }
        CheckDestination();
    }

    void CheckDestination()
    {
        for (int i = 0; i < actionTriggers.Count; i++)
        {
            if (actionTriggers[i] == null)
            {
                isActiveDestination[i] = true;
                buttons[i].GetComponent<MeshRenderer>().material = activeMat;
            }
        }
    }

    void ChangeLocation(int nextSceneNumber)
    {
        if (!GetComponent<OnArriving>().arriving)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<FadeInFadeOut>().StartFadeIn();
            GetComponent<SceneSwitcher>().nextSceneNumber = nextSceneNumber;
            GetComponent<SceneSwitcher>().LoadNextScene();
        }
        else
        {
            GetComponent<OnArriving>().arriving = false;
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
