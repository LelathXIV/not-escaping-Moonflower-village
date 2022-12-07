using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_events : MonoBehaviour
{
    public GameObject actionCollider;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void StopAnimation_1()
    {
        anim.SetBool("onClick", false);
        //add another animation event - call dialogue bubble - commentary
    }
    public void StopAnimation_questItemUsed()
    {
        anim.SetBool("questItemUsed", false);
        //add another event
        actionCollider.gameObject.SetActive(false);
    }
    
}
