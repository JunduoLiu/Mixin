using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * QuestObjectTrigger  ?????????????
 * 
 * 
 */

public class QuestObjectTrigger : MonoBehaviour     
{
    //public GameObject objectToActivate;

                                                                                                                                                                   public string questToCheck;

    public string unlockWhenCompleted;

    //public bool activeIfComplete;

    private bool initialCheckDone;                                                                                                                                                                                                                                                                                      

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!initialCheckDone)
		{
            initialCheckDone = true;
            CheckCompeletion();
		}
    }

    public void CheckCompeletion()
	{
		if (QuestManager.instance.CheckIfCompelete(questToCheck))
		{
            //objectToActivate.SetActive(activeIfComplete);
            if (unlockWhenCompleted != "None")
			{
                QuestManager.instance.MarkQuestUnlocked(unlockWhenCompleted);
               
			}
		}
	}
}
