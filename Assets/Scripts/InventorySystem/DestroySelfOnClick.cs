using UnityEngine;
using UnityEngine.UI;

public class DestroySelfOnClick : MonoBehaviour
{
    //this script is made to destroy instantiated objects on click
    float time;
    public Image image;

    private void Start()
    {
        image.GetComponent<Image>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        image.color = new Color(1,1,1, ((3-time)/3));
        if (time >= 3)
        {
            Destroy(gameObject);
        }
    }
}
