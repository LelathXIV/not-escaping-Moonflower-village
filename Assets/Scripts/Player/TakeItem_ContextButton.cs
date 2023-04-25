using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeItem_ContextButton : MonoBehaviour
{
    public InventoryObject playerInventory;
    [SerializeField] public GameObject ContextImag_TakeItem;
    public int amount;
    public List<GameObject> itemsToCollect;
    private Button btn;

    void Start()
    {
        btn = ContextImag_TakeItem.GetComponent<Button>();
        StartCoroutine(LateStart(1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
        for (int i = 0; i < itemsToCollect.Count; i++)
        {
            if (itemsToCollect[i] == null)
            {
                itemsToCollect.Remove(itemsToCollect[i]);
            }
        }
        ContextImag_TakeItem.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "collectable")
        {
            print(other.tag);
            itemsToCollect.Add(other.gameObject);
            ContextImag_TakeItem.gameObject.SetActive(true);
            btn.onClick.AddListener(PickUpItem);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "collectable") //change action icon and actions here
        {
            itemsToCollect.Remove(other.gameObject);
            ContextImag_TakeItem.gameObject.SetActive(false);
        }
    }

    public void PickUpItem()
    {
        //add pick-up effect here
        //сохранять состояние объекта в itemActionText
        btn.onClick.RemoveListener(PickUpItem); //может быть прблема в листенере и его удалении
        GetComponent<PlayerAnimations>().Gather();
        for (int i = 0; i < itemsToCollect.Count; i++)
        {
            itemsToCollect[i].GetComponent<Items_InWorld>().AddToInventory();
        }
        itemsToCollect.Clear();
        ContextImag_TakeItem.gameObject.SetActive(false);
    }
}
