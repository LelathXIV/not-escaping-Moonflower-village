using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject.SpaceFighter;

public class OnArriving : MonoBehaviour
{
    public bool arriving;
    public GameObject trainZoom;
    public GameObject player;
    public Camera currentSceneCamera;

    void OnLevelWasLoaded()
    {
        if (arriving)
        {
            StartCoroutine(TurnOnTrainZoom());
        }
    }

    IEnumerator TurnOnTrainZoom()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var playerZoom = player.GetComponent<Zoom_ContextButton>();
        var zoomCamera = trainZoom.transform.Find("Camera");
        playerZoom.zoomCamera = zoomCamera.gameObject;
        playerZoom.currentSceneCamera = currentSceneCamera;
        playerZoom.TurnZoomOn();
        yield return new WaitForSeconds(2);
        playerZoom.zoomUI.SetActive(true);
    }
}
