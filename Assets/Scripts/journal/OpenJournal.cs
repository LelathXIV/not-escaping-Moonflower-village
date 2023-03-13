using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenJournal : MonoBehaviour
{
    public GameObject JournalTab;
    public GameObject StatsTab;
    public void OpenJournalButton()
    {
        JournalTab.SetActive(true);
        JournalTab.transform.SetAsLastSibling();
    }
}
