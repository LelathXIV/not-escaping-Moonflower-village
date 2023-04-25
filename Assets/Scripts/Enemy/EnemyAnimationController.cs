using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator enemyAnimator;
    public bool doingSmth;
    public float interactionLength;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    public void Idle()
    {
        enemyAnimator.SetFloat("speed", 0);
    }

    public void Run()
    {
        enemyAnimator.SetFloat("speed", 1);
    }

    public void TakeHit()
    {
        enemyAnimator.SetTrigger("getHit");
        WaitForEndOfClip();
    }

    public void Attack_1()
    {
        enemyAnimator.SetTrigger("attack_1");
        WaitForEndOfClip();
    }

    public void Attack_2()
    {
        enemyAnimator.SetTrigger("attack_2");
        WaitForEndOfClip();
    }

    public void Die()
    {
        enemyAnimator.SetTrigger("die");
        WaitForEndOfClip();
    }

    IEnumerator WaitForEndOfClip()
    {
        doingSmth = true;
        yield return new WaitForSeconds(0.1f); //wait for anim to transit
        var m_CurrentClipInfo = enemyAnimator.GetCurrentAnimatorClipInfo(0);
        //Access the current length of the clip
        //for some FKING reason it shows stats of anim before transition
        float m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        print(m_CurrentClipLength);
        interactionLength = m_CurrentClipInfo[0].clip.length;
        yield return new WaitForSeconds(m_CurrentClipInfo[0].clip.length);
        doingSmth = false;
        StopAllCoroutines();
    }
}
