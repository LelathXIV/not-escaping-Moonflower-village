using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkOverObstaclesStart : MonoBehaviour
{
    private LayerMask ignoredColliders;
    private RaycastHit hitTarget;
    public GameObject playerGameobject;
    public GameObject goOverLogsCollider;
    private Animator anim;
    public GameObject UI;
    public GameObject openInventoryButton;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera camera = GetComponent<Camera>();

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("collectable"))) //все собирашки помещать только на слой collectable
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
        switch (hit.transform.tag)
        {
            case ("backpack"): //unique event in the begginng
                Destroy(hit.transform.gameObject);
                playerGameobject.transform.Find("Backpack").transform.gameObject.SetActive(true);
                //close zoom
                //backpack tutorial
                goOverLogsCollider.gameObject.SetActive(true);
                openInventoryButton.gameObject.SetActive(true);
                break;
            case ("actionTrigger"):
                //there is a bug - animation playes even if u click "close zoom"  - but thats ok
                anim.SetBool("isPlaying", true);
                UI.gameObject.SetActive(false);
                break;

        }
    }

    //return UI method
    //stop animation method (call event) - another cript ( + call dialogue)
}
