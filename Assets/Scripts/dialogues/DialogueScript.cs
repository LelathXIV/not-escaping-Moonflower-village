using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public List<string> lines;
    public TextMeshProUGUI speaker1;
    public TextMeshProUGUI speaker2;
    public TextMeshProUGUI activeSpeaker;
    public TextMeshProUGUI textBuble;
    public int x;
    public bool mainDialogueFinished;

    public void StartDialogue()
    {
        x = 0;
        activeSpeaker = speaker1;
        AssemblingDialogue(x);
    }

    public void NextDialogue()
    {
        x += 2;
        if(x < lines.Count)
        {
            CheckSpeaker(x);
        }
        else
        {
            gameObject.SetActive(false);
            x = 0;
        }
    }

    void CheckSpeaker(int x)
    {
        if (x >= 2 && lines[x] != lines[x-2])
        {
            print(lines[x]);
            if(activeSpeaker == speaker1)
            {
                speaker1.transform.parent.gameObject.SetActive(false);
                speaker2.transform.parent.gameObject.SetActive(true);
                activeSpeaker = speaker2;
            }
            else if (activeSpeaker == speaker2)
            {
                speaker2.transform.parent.gameObject.SetActive(false);
                speaker1.transform.parent.gameObject.SetActive(true);
                activeSpeaker = speaker1;
            }
        }
        AssemblingDialogue(x);
    }

    public void AssemblingDialogue(int x)
    {
        activeSpeaker.text = lines[x];
        textBuble.text = lines[x + 1];
    }
}
