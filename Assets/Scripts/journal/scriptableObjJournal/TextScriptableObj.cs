using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScriptableObj : ScriptableObject
{
    public Sprite textImageElement;
    [TextArea(5,2)]
    public string textElement;
    public string questName;
    public StoryType type;
}

public enum StoryType
{
    diary,
    clue,
    story,
    stats
}