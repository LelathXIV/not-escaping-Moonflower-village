using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDragnDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private GameObject movableDuplicate;
    private GameObject canvasGameObject;
    private Canvas canvas;
    private GameObject duplicate;
    private Camera cameraActive;
    private GameObject player;
    private void Awake()
    {
        movableDuplicate = Resources.Load("movableDuplicate") as GameObject;
        canvas = GameObject.FindGameObjectWithTag("canvas").GetComponent<Canvas>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //drags copy on screen canvas
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //place duplicate item action here
        cameraActive = player.GetComponent<PlayerController>().activeCamera;
        RaycastHit hit;
        Ray ray = cameraActive.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("collectable"))) //everything collectable - to collectable layer
        {
            if (hit.transform.tag == "actionTrigger")
            {
                OnDropAction(hit);
            }
        }
        Destroy(duplicate);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //creates visual copy of an inventory object
        movableDuplicate.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
        duplicate = Instantiate(movableDuplicate, this.transform.position, Quaternion.identity ,this.transform.parent.transform.parent.transform.parent);
        rectTransform = duplicate.GetComponent<RectTransform>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

    }
   
    private void OnDropAction(RaycastHit hit)
    {
        //this is gonna be a very big switch/case
        if (hit.transform.gameObject.GetComponent<QuestColliders>().expectedItem == this.GetComponent<InventoryItemAction>().item)
        {
            hit.transform.gameObject.GetComponent<QuestColliders>().targetObject.GetComponent<Animator>().SetBool("questItemUsed", true);
        }
    }
}
