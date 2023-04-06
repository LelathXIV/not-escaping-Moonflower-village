// Script by Marcelli Michele

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class MoveRuller : MonoBehaviour
{
    public Camera thisZoomCamera;
    GameObject ruller1;
    GameObject ruller2;
    GameObject ruller3;
    GameObject ruller4;
    Dictionary<GameObject, int> rulerToIntKeeper;
    Dictionary<float, int> rotationsToNumber;
    public int[] password = new int [] { 0,0,0,0 };

    public GameObject parent;
    public bool isFinished;

    void Awake()
    {
        ruller1 = GameObject.Find("Ruller1");
        ruller2 = GameObject.Find("Ruller2");
        ruller3 = GameObject.Find("Ruller3");
        ruller4 = GameObject.Find("Ruller4");
        rotationsToNumber = new Dictionary<float, int>
        {
            { -144,0},
            { -180,1},
            {  144,2},
            { 108,3},
            { 72,4},
            { 36,5},
            { 0,6},
            { -36,7},
            { -72,8},
            { -108,9}
        };
        rulerToIntKeeper = new Dictionary<GameObject, int>
        {
            { ruller1, rotationsToNumber[UnityEditor.TransformUtils.GetInspectorRotation(ruller1.transform).x] },
            { ruller2, rotationsToNumber[UnityEditor.TransformUtils.GetInspectorRotation(ruller1.transform).x] },
            { ruller3, rotationsToNumber[UnityEditor.TransformUtils.GetInspectorRotation(ruller1.transform).x] },
            { ruller4, rotationsToNumber[UnityEditor.TransformUtils.GetInspectorRotation(ruller1.transform).x] }
        };
    }

    private void Start()
    {
        for (int i = 0; i < SaveGameManager.CurrentSaveData._padlockMGSaveData.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._padlockMGSaveData[i].isFinished == true)
            {
                isFinished = true;
                parent.GetComponent<OpenDoor>().RunAnimation();
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFinished)
        {
            RaycastHit hit;
            Ray ray = thisZoomCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("zoomCamera")))
            {
                if (hit.transform.tag == "miniGame")
                {
                    RotateRulers(hit.transform.gameObject);
                }
            }
        }

        void RotateRulers(GameObject ruler)
        {
            ruler.transform.Rotate(-36, 0, 0, Space.Self);
            rulerToIntKeeper[ruler] += 1;
            if (rulerToIntKeeper[ruler] > 9)
            {
                rulerToIntKeeper[ruler] = 0;
            }
            CheckPassword();
        }

        void CheckPassword()
        {
            if    (rulerToIntKeeper[ruller1] == password[0] &&
                   rulerToIntKeeper[ruller2] == password[1] &&
                   rulerToIntKeeper[ruller3] == password[2] &&
                   rulerToIntKeeper[ruller4] == password[3])
            {
                print("you won!");
                isFinished = true;
                parent.GetComponent<OpenDoor>().RunAnimation();
                SaveMGStatus();
            }
        }
    }

    void SaveMGStatus()
    {
        var saveExists = false;
        for (int i = 0; i < SaveGameManager.CurrentSaveData._padlockMGSaveData.Count; i++)
        {
            if (SaveGameManager.CurrentSaveData._padlockMGSaveData[i].position == transform.position)
            {
                saveExists = true;
            }
        }
        if (!saveExists)
        {
            var thisMGSaveData = new PadlockMGSaveData();
            thisMGSaveData.position = transform.position;
            thisMGSaveData.isFinished = isFinished;
            SaveGameManager.CurrentSaveData._padlockMGSaveData.Add(thisMGSaveData);
            SaveGameManager.SaveGame();
        }
    }
}

[System.Serializable]
public class PadlockMGSaveData
{
    public bool isFinished;
    public Vector3 position;
}
