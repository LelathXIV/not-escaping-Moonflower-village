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
    public GameObject playerBody;
    public List<GameObject> UIElements;
    public bool isInZoom;

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
    public void TurnZoomOn()
    {
        playerBody.SetActive(false); //turns off player body for zoom
        GetComponent<CharacterController>().radius = 0;
        GetComponent<PlayerController>().activeCamera.gameObject.SetActive(false);
        zoomCamera.SetActive(true);
        for (int i = 0; i < UIElements.Count; i++)
        {
            UIElements[i].gameObject.SetActive(false);
        }
        closeZoom = zoomUI.transform.Find("CloseZoom").gameObject;
        closeZoom.GetComponent<Button>().onClick.AddListener(CloseZoom);
        zoomUI.SetActive(true);
        isInZoom = true;
    }
    public void CloseZoom()
    {
        playerBody.SetActive(true); //turns back on the player body
        GetComponent<CharacterController>().radius = 0.2f;
        GetComponent<PlayerController>().activeCamera = currentSceneCamera;
        GetComponent<PlayerController>().activeCamera.gameObject.SetActive(true);
        for (int i = 0; i < UIElements.Count; i++)
        {
            UIElements[i].gameObject.SetActive(true);
        }
        zoomCamera.SetActive(false);
        zoomUI.SetActive(false);
        isInZoom = false;
    }

    public void ClearContextActionWindow()
    {
        ContextImag_zoomIn.gameObject.SetActive(false);
        btn.onClick.RemoveAllListeners();
    }
}
