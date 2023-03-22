using UnityEngine;
using UnityEngine.UI;

public class ActionContextButton : MonoBehaviour
{
    public GameObject contextActionImage;
    public GameObject targetObject;
    public Button btn;

    private void Start()
    {
        btn = contextActionImage.GetComponent<Button>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "contextAction")
        {
            contextActionImage.gameObject.SetActive(true);
            targetObject = other.transform.GetComponent<ContextActionTrigger>().targetObject;
            other.transform.GetComponent<ContextActionTrigger>().SaveQuestColliderData();
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
        targetObject.GetComponent<Animator>().SetTrigger("Active");
    }
}
