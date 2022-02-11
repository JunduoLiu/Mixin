using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Quest marker marks the corresponding quest compeleted
 * when certain conditions are met 
 * 
 */


public class QuestMarker : MonoBehaviour
{

    public string questToMark;
    //public bool markCompelete;
    public bool markOnEnter;
    //public bool markOnClick;
    public bool markAfterDialogue;
    public string[] prequests;
    private bool canMark;
	private bool canUnlock;
    

    //public bool deactiveOnMarking;

    //public static QuestMarker instance;
    // Start is called before the first frame update
    void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
	{
		
	}

	public void MarkQuest()
	{
		Debug.Log("Marking " + questToMark + "compelete using Quest Marker");
         QuestManager.instance.MarkQuestCompelete(questToMark);

        //gameObject.SetActive(deactiveOnMarking);
    }

    //return false if there are incomplete prequests
    public bool CheckPrequests()
	{
        //check if any of the prequest is incomplete
        foreach (string questName in prequests)
		{
			if (!QuestManager.instance.CheckIfCompelete(questName))
			{
				Debug.Log(questToMark + " can't not be unlocked, because " + questName + "is incompleted");
				return false;
			}
		}

		QuestManager.instance.MarkQuestUnlocked(questToMark);
		//if all the prequests are complete, unlock this quest
		return true;
	}

	//private void OnTriggerEnter2D(Collider2D other)
	//{
	//	if (other.tag == "Player")
	//	{
	//		if (markOnEnter)
	//		{
	//			MarkQuest();

	//		}
	//		else
	//		{
	//			canMark = true;
	//		}
	//	}
	//}

	//private void OnTriggerExit2D(Collider2D other)
	//{
	//	if (other.tag == "Player")
	//	{
	//		canMark = false;
	//	}
	//}
}
