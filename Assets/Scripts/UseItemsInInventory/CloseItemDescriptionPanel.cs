using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseItemDescriptionPanel : MonoBehaviour
{
    public void CloseDescriptionPanel()
    {
       Destroy(transform.parent.transform.parent.gameObject);
    }
}
