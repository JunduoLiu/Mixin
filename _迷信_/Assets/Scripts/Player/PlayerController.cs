using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CodeMonkey.Utils;
using System;

/*
 * Player movement with Left and Right Arrow keys
 * Open Bag with Key B
 * */

public class PlayerController : MonoBehaviour
{
	[Space]
    [Header("Character attributes:")]
    public float MOVEMENT_BASE_SPEED = 1.0f;

    [Space]
    [Header("Character statistics:")]
    public Vector2 movementDirection;
    public float movementSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

	[Space]
	[Header("Inventory:")]
	public Inventory inventory;
	public GameObject inventoryUI;

	[Space]
	[Header("Player Status:")]
	public IntVariable iReputation;
	public IntVariable iMoral;
	public int pickupPunish;

	private bool isFrozen;

	void Awake()
	{
		isFrozen = false;
	}

    void Start()
    {
		iReputation.IntVariableReachedZero.AddListener(OnReputationReachedZero);
		iMoral.IntVariableReachedZero.AddListener(OnMoralReachedZero);
		iReputation.IntVariableChanged.AddListener(OnReputationChanged);
		iMoral.IntVariableChanged.AddListener(OnMoralChanged);
	}

    void Update()
    {
		ProcessInputs();
		if (!isFrozen) // freeze player movement when ui active
		{
			Move();
			Animate();
		}

		if (Input.GetKeyDown(KeyCode.B)){
			inventoryUI.SetActive(!inventoryUI.activeSelf);
			isFrozen = inventoryUI.activeSelf;
		}

		// if (Input.GetKeyDown(KeyCode.LeftBracket)){
		// 	inventory.Save();
		// }
		// if (Input.GetKeyDown(KeyCode.RightBracket)){
		// 	inventory.Load();
		// }
    }

    void ProcessInputs(){
      movementDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
      movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
      movementDirection.Normalize();
    }

    void Move(){
		rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    void Animate(){
		animator.SetFloat("AniMoveX", movementDirection.x);
	    animator.SetFloat("AniMoveY", movementDirection.y);
    }

	void OnReputationChanged()
    {
		Debug.Log("名誉值产生了变化");
    }

	void OnMoralChanged()
	{
		Debug.Log("道德值产生了变化");
	}

	private void OnTriggerEnter2D(Collider2D _collider)
	{
        PickupTrigger trigger = _collider.GetComponent<PickupTrigger>();
        if (trigger != null && !trigger.HasPickedUp())
        {
            System.Random rand = new System.Random();
			float f = (float)rand.NextDouble();
			Debug.Log("这有个好东西..." + f.ToString());
            if (f < trigger.stealRate)
            {
                trigger.pickupLocation.s_item.stolenScene = Loader.StringToScene(gameObject.scene.name);
                inventory.AddItem(trigger.pickupLocation.s_item, 1);
                trigger.pickupLocation.HasPickedUp = true;
				Debug.Log("偷走一个" + trigger.pickupLocation.s_item.itemName);
				PickUpPunish();
            }

        }
    }

	void OnReputationReachedZero()
    {
		// GG
    }

	void OnMoralReachedZero()
	{
		// 说胡话
	}

	private void PickUpPunish()
    {
		iMoral.ReduceValue(pickupPunish);
    }

	public Vector3 GetPosition() {
        return transform.position;
    }

}
