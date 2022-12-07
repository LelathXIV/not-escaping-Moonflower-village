using System.Collections;
using System.IO;
using UnityEngine;

public class ScreenShotURP : MonoBehaviour
{
    public KeyCode screenshotKey;
    private Camera _camera;
    public int screenCounter;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            StartCoroutine(Capture());
        }
    }

    IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
        RenderTexture activeRenderTexture = RenderTexture.active;
        Debug.Log(_camera);
        RenderTexture.active = _camera.targetTexture;

        _camera.Render();

        Texture2D image = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
        image.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);

        File.WriteAllBytes(Application.persistentDataPath + "/" + screenCounter + "_output.png", bytes);
        screenCounter++;
        Debug.Log(Application.persistentDataPath);
    }
}