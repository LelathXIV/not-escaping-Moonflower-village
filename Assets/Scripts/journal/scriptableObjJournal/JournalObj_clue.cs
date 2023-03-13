using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Journal", menuName = "Journal/Clue")]

public class JournalObj_clue : TextScriptableObj
{
    private void Awake()
    {
        type = StoryType.clue;
    }
}
