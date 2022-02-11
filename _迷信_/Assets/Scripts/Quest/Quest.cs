using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public string questName;
    public int questIndex;

    [TextArea(3, 10)]
    public string[] sentences;

    private bool isUnlocked;
    private bool isCompeleted;
}
