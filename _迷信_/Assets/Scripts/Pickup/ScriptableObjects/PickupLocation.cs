using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Pickup Location", menuName = "InventorySystem/PickupLocation")]
public class PickupLocation : ScriptableObject
{
    public StolenItem s_item;

    [SerializeField]
    private bool hasPickedUp;

    public bool HasPickedUp { get => hasPickedUp; set => hasPickedUp = value; }

    private void Awake()
    {
        hasPickedUp = false; // Only for testing
    }



}
