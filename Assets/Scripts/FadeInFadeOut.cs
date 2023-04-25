using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
    public Image fadeImg;
    public float fadeSpeed;

    private void Start()
    {
        StartFadeOut();
        print("in this script lies a fadeIn function");
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        fadeImg.gameObject.SetActive(true);

        var fadingIn = true;
        float t = fadeSpeed;
        Color c = fadeImg.color;
        fadeImg.color = c;
        while (fadingIn)
        {
            c.a = t;
            t -= Time.deltaTime;
            fadeImg.color = c;
            if (t <= 0)
            {
                fadingIn = false;
                fadeImg.gameObject.SetActive(false);
            }
            yield return null;
        }
        StopAllCoroutines();
    }

    IEnumerator FadeIn() //make coroutines
    {
        fadeImg.gameObject.SetActive(true);

        var fadingIn = true;
        float t = 0;
        Color c = fadeImg.color;
        fadeImg.color = c;

        while (fadingIn)
        {
            c.a = t;
            t += Time.deltaTime;
            fadeImg.color = c;
            if (t > fadeSpeed)
            {
                fadingIn = false;
                print("choo choo");
            }
            yield return null;
        }
        StopAllCoroutines();
    }
}
