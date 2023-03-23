using UnityEngine;
using UnityEngine.UI;

public class ActionContextButton : MonoBehaviour
{
    public GameObject contextActionImage;
    public Button btn;
    GameObject actionCollider;

    private void Start()
    {
        btn = contextActionImage.GetComponent<Button>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "contextAction")
        {
            contextActionImage.gameObject.SetActive(true);
            actionCollider = other.gameObject;
            btn.onClick.AddListener(Action);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "contextAction")
        {
            contextActionImage.gameObject.SetActive(false);
            btn.onClick.RemoveAllListeners();
        }
    }

    void Action()
    {
        actionCollider.GetComponent<ContextActionTrigger>().RunAction();
        contextActionImage.gameObject.SetActive(false);
        btn.onClick.RemoveAllListeners();
    }
}
