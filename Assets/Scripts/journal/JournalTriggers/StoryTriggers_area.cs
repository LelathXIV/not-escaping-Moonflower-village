using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTriggers_area : MonoBehaviour
{
   public JournalObj_story journalTextElement;
  
   private void Start()
   {
        var savedStoryPages = SaveGameManager.CurrentSaveData._playerSavedStoryPages;
        if (journalTextElement == null)
        {
            GameObject.Destroy(GetComponent<StoryTriggers_area>());
        }

        foreach (Journal_Page page in savedStoryPages)
        {
            if(page.questName == journalTextElement.questName && page.thisStoryElements.Contains(journalTextElement))
            {
                GameObject.Destroy(GetComponent<StoryTriggers_area>());
            }
        }
    }
  
   private void OnTriggerEnter(Collider other)
   {
       if(other.tag == "Player")
       {
           var journal = GameObject.FindGameObjectWithTag("journal").GetComponent<Journal>();
           var notFound = true;
           var _listOfStoryPages = journal.listOfStoryPages;
           Debug.Log(_listOfStoryPages);

           foreach(Journal_Page page in _listOfStoryPages)
           {
               if(page.questName == journalTextElement.questName)
               {
                    notFound = false;
                   if (page.thisStoryElements.Contains(journalTextElement))
                       { Debug.Log("already have this page!!"); }
                   else
                   {
                       page.thisStoryElements.Add(journalTextElement);
                   }
               }
           }

           if (notFound)
           {
                var newPage = new Journal_Page();

                newPage.pageNumber = _listOfStoryPages.Count;

                newPage.questName = journalTextElement.questName;

                var listOfElements = new List<JournalObj_story>();

                listOfElements.Add(journalTextElement);

                newPage.thisStoryElements = listOfElements;
                _listOfStoryPages.Add(newPage);
           }
           
           TriggerChecked(journal);
       }
   }
  
  public void TriggerChecked(Journal journal)
  {
      SaveGameManager.CurrentSaveData._playerSavedStoryPages = journal.listOfStoryPages;
      GameObject.Destroy(GetComponent<StoryTriggers_area>());
      SaveGameManager.SaveGame();
  }

}
