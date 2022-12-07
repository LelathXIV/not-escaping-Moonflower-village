using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBaclToPlayer : MonoBehaviour
{
    private GameObject player;
    public GameObject UI;
    public GameObject joyStick;
    public GameObject zoomUI;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MovePlayer()
    {
        UI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        gameObject.GetComponentInParent<BoxCollider>().isTrigger = false;
        player.gameObject.SetActive(false);
         //teleports player to where animation ends - doesnt work!?
        player.gameObject.transform.position = transform.position;
        player.GetComponent<ContextActionSystem>().ClearContextActionWindow();
        player.GetComponent<ContextActionSystem>().CloseZoom();
        player.gameObject.SetActive(true);
    }
}
