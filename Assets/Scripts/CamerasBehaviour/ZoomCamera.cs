using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    private void Update()
    {
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
        }
    }
}

