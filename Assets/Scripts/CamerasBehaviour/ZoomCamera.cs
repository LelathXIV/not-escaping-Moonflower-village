using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;

public class ZoomCamera : MonoBehaviour
{
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject(5))    // is the touch on the GUI
        {
            // GUI Action
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Camera camera = GetComponent<Camera>();

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("zoomCamera"))) //everything collectable - to collectable layer
            {
                if (hit.transform)
                {
                    OnClickAction(hit);
                }
            }
        }

    }

    private void OnClickAction(RaycastHit hit)
    {
        switch(hit.transform.tag)
        {
            case ("collectable"):
                Debug.Log(hit.transform.tag);
                hit.transform.gameObject.GetComponent<Items_InWorld>().AddToInventory(); 
                hit.transform.gameObject.GetComponent<ItemsInWorldLoader>().ObjectIsTaken();
                break;
            case ("openable"):
                Debug.Log(hit.transform.tag);
                hit.transform.GetComponent<OpenObject>().RunAnimation();
                break;
            case ("actionTrigger"):
                Debug.Log(hit.transform.name);
                //включать хинты if no item
                hit.transform.GetComponent<QuestColliders>().CheckIfItemIsInInventory();
                break;

        }
    }
}

