using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset;

    public void Start()
    {
        cameraOffset = new Vector3(0, 15, -7);
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.transform.position + cameraOffset;
        transform.position = newPosition;
    }
}
