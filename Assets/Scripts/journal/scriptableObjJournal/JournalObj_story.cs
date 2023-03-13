using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Journal", menuName = "Journal/Story")]

public class JournalObj_story : TextScriptableObj
{
    private void Awake()
    {
        type = StoryType.story;
    }
}
