using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

    private bool canInteract;

	public bool needToMarkQuest;
	//public string quest;

	public bool needToAddToBag;

	public QuestMarker theMarker;
	//public bool markCompelete;

    void Update()
	{
		//if player is within the area, and mouse 0 is clicked
		if (canInteract && Input.GetButton("Fire1"))
		{
			Debug.Log("triggering dialogue");
			canInteract = false;
			TriggerDialogue();
		}
	}

	public void TriggerDialogue()
	{
		DialogueManager theDM = FindObjectOfType<DialogueManager>();


		if (needToMarkQuest) //check if this dialogue has a linked quest
		{
			if (theMarker.prequests.Length != 0) //check if there is any prequests
			{

				if (!theMarker.CheckPrequests())
				{
                    //if there are incomplete prequests
					return;
				}
			}

			string quest = theMarker.questToMark;
			theDM.StartDialogue(dialogue, quest);


		}
		else
		{
			Debug.Log("this item does not have a quest");
			theDM.StartDialogue(dialogue, null);
		}

		if (needToAddToBag)
		{
			// theDM.sentences.Enqueue("带不带着？");
			// theDM.objectNames.Enqueue("");
			// // tell DM which item/obj to add/Destroy
			// theDM.triggerItem = gameObject.GetComponent<ItemWorld>().item;
			// theDM.triggerObj = gameObject;
		}

		//if there is a quest that needs to be marked at the end the of convo
		if (needToMarkQuest)
		{
			theDM.ActivateQuestAtEnd();
		}



	}


	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.tag == "Player")
		{
			canInteract = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			canInteract = false;
		}
	}
}
