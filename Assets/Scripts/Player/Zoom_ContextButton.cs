using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom_ContextButton : MonoBehaviour
{
    private Button btn;
    [SerializeField] GameObject contextActionButton;
    public Transform ContextImag_zoomIn;

    public GameObject zoomCamera;
    public GameObject zoomUI;
    private GameObject screenJoyStick;
    public Camera currentSceneCamera;
    private GameObject closeZoom;
    public InventoryObject playerInventory;


    private void Awake()
    {
        btn = contextActionButton.GetComponent<Button>();

        ContextImag_zoomIn.gameObject.SetActive(false);

        zoomUI.SetActive(false);
        screenJoyStick = GameObject.FindGameObjectWithTag("ScreenJoyStick");
    }

    private void OnTriggerEnter(Collider other) //add/change context image (later/if)
    {
        //entering zoom zone
        if(other.tag == "zoomZone")
        {
            ContextImag_zoomIn.gameObject.SetActive(true);
            zoomCamera = other.transform.Find("Camera").gameObject;
            currentSceneCamera = gameObject.GetComponent<PlayerController>().activeCamera;
            btn.onClick.AddListener(TurnZoomOn);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "zoomZone") //change action icon and actions here
        {
            ContextImag_zoomIn.gameObject.SetActive(false);
            btn.onClick.RemoveAllListeners();
        }
    }

    private void TurnZoomOn()
    {
        GetComponent<PlayerController>().activeCamera.gameObject.SetActive(false);

        zoomCamera.gameObject.SetActive(true);
        screenJoyStick.gameObject.SetActive(false);
        zoomUI.gameObject.SetActive(true);
        contextActionButton.SetActive(false);

        closeZoom = zoomUI.transform.Find("CloseZoom").gameObject;
        closeZoom.GetComponent<Button>().onClick.AddListener(CloseZoom);
    }

    public void CloseZoom()
    {
         GetComponent<PlayerController>().activeCamera = currentSceneCamera;

         GetComponent<PlayerController>().activeCamera.gameObject.SetActive(true);

         zoomCamera.gameObject.SetActive(false);
         screenJoyStick.gameObject.SetActive(true);
         zoomUI.gameObject.SetActive(false);
         contextActionButton.SetActive(true);
    }

    public void ClearContextActionWindow()
    {
        ContextImag_zoomIn.gameObject.SetActive(false);
        btn.onClick.RemoveAllListeners();
    }
}
