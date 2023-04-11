using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject canvas;
    public Image fadeImg;
    public int nextSceneNumber;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("canvas");
    }

    public void LoadNextScene()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds (2);

        switch (nextSceneNumber)
        {
            case (0):
                SceneManager.LoadScene("Start_scene");
                break;
            case (1):
                SceneManager.LoadScene("Oak_scene");
                break;
        }
    }
}
