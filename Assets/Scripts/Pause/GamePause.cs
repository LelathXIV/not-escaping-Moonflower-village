using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour, IGamePauseService
{
    public bool isFrozen { get; set; }
    private IEnumerator pauseCoroutine;

    private void Awake()
    {
        pauseCoroutine = DoFreeze();
    }

    public void Freeze()
    {
        isFrozen = true;
        StartCoroutine(pauseCoroutine);
    }

    public void UnFreeze()
    {
        isFrozen = false;
        StopCoroutine(pauseCoroutine);
        Time.timeScale = 1;
    }

    IEnumerator DoFreeze()
    {
        while (isFrozen == true)
        {
            Time.timeScale = 0;
            yield return new WaitForEndOfFrame();
        }
    }
}
