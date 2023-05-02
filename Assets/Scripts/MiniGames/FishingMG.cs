using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingMG : MonoBehaviour
{
    public Camera thisZoomCamera;
    public GameObject MGStarterCollider;
    public Button catchFish;
    public GameObject fishingSircle;
    public GameObject fishingMGUI;
    public bool keyObtained;
    public List<GameObject> listOfRewards;
    public ItemObject questItem;
    public ItemObject fish;
    bool scaling;

    private void Start()
    {
        if(SaveGameManager.CurrentSaveData.gotQuestItemFromPond == true)
        {
            keyObtained = true;
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = thisZoomCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10, 1 << LayerMask.NameToLayer("zoomCamera")))
            {
                if (hit.transform == MGStarterCollider.transform)
                {
                    StartMG();
                }
            }
        }
    }

    void StartMG()
    {
        fishingMGUI.SetActive(true);
        MGStarterCollider.SetActive(false);
        scaling = true;
        StartCoroutine(ScalingCircle());
        catchFish.onClick.AddListener(CatchFish);
    }

    IEnumerator ScalingCircle()
    {
        while (scaling)
        {
            fishingSircle.transform.localScale = new Vector3(
                Mathf.PingPong(Time.time * 2, 1),
                Mathf.PingPong(Time.time * 2, 1),
                0);
            yield return null;
        }
    }

    void CatchFish()
    {
        catchFish.onClick.RemoveListener(CatchFish);
        scaling = false;
        MGStarterCollider.SetActive(true);
        StopCoroutine(ScalingCircle());
        fishingMGUI.SetActive(false);
        CalculateWin();
    }

    void CalculateWin()
    {
        if (fishingSircle.transform.localScale.x <= 0.18 && fishingSircle.transform.localScale.y <= 0.18)
        {
            var playerInventory = Resources.Load("PlayerInventory") as InventoryObject;
            if (!keyObtained)
            {
                keyObtained = true;
                playerInventory.AddItem(questItem, 1);
                SaveGameManager.CurrentSaveData.gotQuestItemFromPond = true;
                SaveGameManager.SaveGame();
                RewardsVisual(questItem, 1);
            }
            else
            {
                playerInventory.AddItem(fish, 1);
                RewardsVisual(fish, 1);
                SaveGameManager.SaveGame();
            }
            print("you got smth");
        }
        else
            print("try again!");
    }

    void RewardsVisual(ItemObject item, int amount)
    {
        var canvas = GameObject.FindGameObjectWithTag("canvas");
        var pickedItemsImages = canvas.transform.Find("pickedItemsImages");
        var pickedItemPanel = Resources.Load("Prefabs/PanelsPrefabs/pickedObjectPrefab") as GameObject;
        var landedImg = Instantiate(pickedItemPanel, pickedItemsImages);
        landedImg.transform.Find("image").GetComponent<Image>().sprite = item.itemImage;
        if (amount > 1)
        { landedImg.transform.Find("amount").GetComponent<TextMeshProUGUI>().text = amount.ToString(); }
    }           

}
