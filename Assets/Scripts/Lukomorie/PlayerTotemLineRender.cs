using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerTotemLineRender : MonoBehaviour
{
    public Transform player;
    public Transform totem;
    public LineRenderer lineRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        totem = GameObject.FindGameObjectWithTag("totem").transform;
        lineRenderer.SetPosition(0, totem.transform.position);
        lineRenderer.SetPosition(1, player.transform.position);
    }
}
