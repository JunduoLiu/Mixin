using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public GameObject dialogueBox;
	public GameObject pickUpButton;
	public GameObject continueButton;

	public QuestManager theQM;
	public PlayerController thePC;

	public Queue<string> sentences;
	public Queue<string> objectNames;

	private string quest;
	private bool markQuestCompelete;
	private bool shouldMarkQuest;
	public Item triggerItem;
	public GameObject triggerObj;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
		objectNames = new Queue<string>();
		theQM = FindObjectOfType<QuestManager>();
		thePC = FindObjectOfType<PlayerController>();
		dialogueBox.SetActive(false);
	}

	public void StartDialogue (Dialogue dialogue, string questName)
	{
		Debug.Log(" Starting Convo");

		quest = questName;
		if (quest != null)
		{
			if (!theQM.CheckIfUnlocked(quest))
			{
				Debug.Log(quest + " is still locked.");
				return;
			}
		}
		//clear previous sentences
		sentences.Clear();
		objectNames.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}


       foreach(string objectName in dialogue.names)
	    {
			objectNames.Enqueue(objectName);

		}

		//show dialogue box
		dialogueBox.SetActive(true);
		pickUpButton.SetActive(false);
		// thePC.SetFreeze(true);
		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;

		}

		string objectName = objectNames.Dequeue();
		string sentence = sentences.Dequeue();
		Text continueText = continueButton.GetComponent(typeof(Text)) as Text;
		continueText.text = "继续";
        if (sentence == "带不带着？"){
			pickUpButton.SetActive(true);
			continueText.text = "算了";
		}
		StopAllCoroutines();
		//Debug.Log("having conversation with " + objectName+ " (DiaglogueManager)");
		nameText.text = objectName;
		StartCoroutine(TypeSentence(sentence));
	}

	// public void PickUp ()
	// {
	// 	Inventory inventory = thePC.inventory;
	//
	// 	inventory.AddNewItem(triggerItem);
	// 	triggerItem = null;
	// 	// triggerObj = null;
	// }

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

    public void EndDialogue()
    {
        //remove dialogue box
		dialogueBox.SetActive(false);
		// thePC.SetFreeze(true);

        if (shouldMarkQuest)
		{
			shouldMarkQuest = false;

			Debug.Log("marking " + quest + " compelete in Dialogue Manager");
			QuestManager.instance.MarkQuestCompelete(quest);
		}
	}

    public void ActivateQuestAtEnd()
	{
		shouldMarkQuest = true;
	}

	//public void AskIfAddToBag()
	//{
	//	nameText.text = "";
	//	dialogueText.text = "要带着么?";
	//	pickUpButton.SetActive(true);

	//}
}
