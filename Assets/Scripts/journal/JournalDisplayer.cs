using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalDisplayer : MonoBehaviour
{
    public GameObject journal;
    public GameObject Main;
    public GameObject previousPage;
    public GameObject nextPage;
    public GameObject allDisplayedTexts;
    public GameObject images;
    public int positionInList;
    public Journal_Page currentJournalPage;
    public List<Journal_Page> listOfAllStoryPages;

    private void OnEnable()
    {
        listOfAllStoryPages = GameObject.FindGameObjectWithTag("journal").GetComponent<Journal>().listOfStoryPages;
        StoryPages();
    }

    public void StoryPages()
    {
        for (int i = 0; i < listOfAllStoryPages.Count; i++)
        {
            if(listOfAllStoryPages[i].pageNumber == positionInList)
            {
                previousPage.GetComponent<Button>().onClick.AddListener(PreviousPage);
                nextPage.GetComponent<Button>().onClick.AddListener(NextPage);
                currentJournalPage = listOfAllStoryPages[i];
                PageVisualsAssembler();
            }
        }
    }

    public void NextPage()
    {
        RemoveListeners();
        positionInList++;
        //yes i dunno how to limit it otherwise
        if(positionInList > (listOfAllStoryPages.Count - 1))
        {
            positionInList = (listOfAllStoryPages.Count - 1);
        }
        StoryPages();
        PageVisualsAssembler();
    }

    public void PreviousPage()
    {
        RemoveListeners();
        positionInList--;
        //shame on me
        if (positionInList <= 0) positionInList = 0;
        StoryPages();
        PageVisualsAssembler();
    }   

    public void PageVisualsAssembler()
    {
        //clear text
        allDisplayedTexts.GetComponent<TextMeshProUGUI>().text = "";

        //assembling pictures
        foreach (Transform child in images.transform)
        {
            child.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < currentJournalPage.thisStoryElements.Count; i++)
        {
            images.transform.GetChild(i).gameObject.SetActive(true);
            images.transform.GetChild(i).GetComponent<Image>().sprite = currentJournalPage.thisStoryElements[i].textImageElement;
            allDisplayedTexts.GetComponent<TextMeshProUGUI>().text += "\n" + currentJournalPage.thisStoryElements[i].textElement.ToString();
        }
    }
    private void RemoveListeners()
    {
        previousPage.GetComponent<Button>().onClick.RemoveAllListeners();
        nextPage.GetComponent<Button>().onClick.RemoveAllListeners();
    }

}
