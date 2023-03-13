using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseJournal : MonoBehaviour
{
    public GameObject journalTab;

    public void CloseJournalButton()
    {
        journalTab.SetActive(false);
    }
}
