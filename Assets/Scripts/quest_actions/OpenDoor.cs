using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;
    public List<GameObject> listOfTriggers;
    public GameObject zoomCollider;
    public GameObject player;
    public GameObject zoomUI;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    //checks how many action colliders(keyholes) still exists
    //if 0 - mtzns lock is opened
    private void Update()
    {
        for (int i = 0; i < listOfTriggers.Count; i++)
        {
            if(listOfTriggers[i] == null)
            {
                listOfTriggers.Remove(listOfTriggers[i]);
            }
        }
        if(listOfTriggers.Count == 0)
        {
            RunAnimation();
        }
    }

    public void RunAnimation()
    {
        animator.SetTrigger("Active");
        zoomUI.SetActive(false);
    }

    public void TurnOfZoom()
    {
        var playerZoom = player.GetComponent<Zoom_ContextButton>();
        if (playerZoom.isInZoom)
        {
            zoomCollider.GetComponent<Collider>().enabled = false;
            playerZoom.CloseZoom();
            playerZoom.ClearContextActionWindow();
        }
    }
}
