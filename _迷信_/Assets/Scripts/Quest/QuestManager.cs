using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Quest Manager is used to manage all the main funciton related to the quest sys 
 * 
 * 
 */
                                                                                                                                                
public class QuestManager : MonoBehaviour
{
    //all the quests that this scene has 
    public string[] questMarkerNames;
    public bool[] questMarkerCompelete;
    public bool[] questMarkerUnlock;

    public static QuestManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        questMarkerCompelete = new bool[questMarkerNames.Length];
        questMarkerUnlock = new bool[questMarkerNames.Length];
        //mark the first 5 quest unlocked
        for (int i = 0; i < 5; i++)
		{
            questMarkerUnlock[i] = true;
		}

    }

    // Update is called once per frame
    void Update()
    {
    }


    //find the index of the questToFind 
    public int GetQuestNumber(string questToFind)
    {
        for (int i = 0; i < questMarkerNames.Length-1; i++)
        {
            if (questMarkerNames[i] == questToFind)
            {
                //Debug.Log(questToFind + " has index " + i);
                return i;
            }
        }
        //if the loop didn't find the questToFind, print out the name
        Debug.LogError("Quest " + questToFind + " does not exist");
        return -1;
    }

    public bool CheckIfCompelete(string questToCheck)
    {
        int questIndex = GetQuestNumber(questToCheck);
        
        return questMarkerCompelete[questIndex];
    }

    public bool CheckIfUnlocked(string questToCheck)
	{
        int questIndex = GetQuestNumber(questToCheck);
        return questMarkerUnlock[questIndex];
    }

    public void MarkQuestCompelete(string questToMark)
	{
        questMarkerCompelete[GetQuestNumber(questToMark)] = true;
	}

    public void MarkQuestIncompelete(string questToMark)
	{
        questMarkerCompelete[GetQuestNumber(questToMark)] = false;
    }

    public void MarkQuestUnlocked(string questToMark)
	{
        questMarkerUnlock[GetQuestNumber(questToMark)] = true;

    }

	//public void UpdateLocalQuestObjects()
	//{
	//	QuestObjectTrigger[] questObjects = FindObjectsOfType<QuestObjectTrigger>();

	//	if (questObjects.Length < 0)
	//	{
	//		for (int i = 0; i < questObjects.Length; i++)
	//		{
	//			questObjects[i].CheckCompeletion();
	//		}
	//	}
	//}
}
