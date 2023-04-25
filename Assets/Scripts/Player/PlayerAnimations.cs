using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator playerAnimator;
    public bool isInteracting;
    public float interactionLength;
    //all interaction should be 0.5f long (0.5 seconds)
    private void Awake()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    }

    public void Idle()
    {
        playerAnimator.SetFloat("speed", 0);
    }
    //animation
    public void Walk()
    {
        playerAnimator.SetFloat("speed", 0.1f);
    }
    //animation
    public void Run()
    {
        playerAnimator.SetFloat("speed", 0.2f);
    }
    //animation

    public void Aiming()
    {
        playerAnimator.SetFloat("speed", 0.3f);
    }
    //animation
    public void Shoot()
    {
        playerAnimator.SetTrigger("shoot");
        StartCoroutine(WaitForEndOfClip());
    }

    public void Gather()
    {
        playerAnimator.SetTrigger("gathering");
        StartCoroutine(WaitForEndOfClip());
    }

    public void MeleAttac()
    {
        playerAnimator.SetTrigger("meleAttack");
        StartCoroutine(WaitForEndOfClip());
    }

    public void GetHit()
    {
        playerAnimator.SetTrigger("getHit");
        StartCoroutine(WaitForEndOfClip());
    }

    public void Death()
    {
        playerAnimator.SetTrigger("die");
        StartCoroutine(WaitForEndOfClip());
    }

    public void Talk() { }
    public void Push() { }

    IEnumerator WaitForEndOfClip()
    {
        isInteracting = true;
        yield return new WaitForSeconds(0.1f); //wait for anim to transit 
        var m_CurrentClipInfo = playerAnimator.GetCurrentAnimatorClipInfo(0);
        float m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        interactionLength = m_CurrentClipLength;
        yield return new WaitForSeconds(m_CurrentClipLength - 0.1f);
        isInteracting = false;
    }
}
