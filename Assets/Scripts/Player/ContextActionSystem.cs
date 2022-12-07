using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextActionSystem : MonoBehaviour
{
    private Button btn;
    [SerializeField] GameObject contextActionButton;
    public Transform ContextImageHolder;
    public GameObject zoomCamera;
    private GameObject zoomUI;
    private GameObject screenJoyStick;
    public Camera currentSceneCamera;
    private GameObject closeZoom;


    private void Awake()
    {
        contextActionButton = GameObject.FindGameObjectWithTag("contextAction");
        btn = contextActionButton.GetComponent<Button>();
        ContextImageHolder = contextActionButton.transform.Find("ZoomInImage");
        ContextImageHolder.gameObject.SetActive(false);
        zoomUI = GameObject.FindGameObjectWithTag("zoomUI");
        zoomUI.SetActive(false);
        screenJoyStick = GameObject.FindGameObjectWithTag("ScreenJoyStick");
    }

    private void OnTriggerEnter(Collider other) //add/change context image (later/if)
    {
        if(other.tag == "zoomZone")
        {
            ContextImageHolder.gameObject.SetActive(true);
            zoomCamera = other.transform.Find("Camera").gameObject;
            currentSceneCamera = gameObject.GetComponent<PlayerController>().activeCamera;
            btn.onClick.AddListener(TurnZoomOn);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "zoomZone") //change action icon and actions here
        {
            ContextImageHolder.gameObject.SetActive(false);
            btn.onClick.RemoveAllListeners();
        }
    }

    //добавить переклоючение камеры на кнопке в user Interface (context menu)
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
        ContextImageHolder.gameObject.SetActive(false);
        btn.onClick.RemoveAllListeners();
    }
}
