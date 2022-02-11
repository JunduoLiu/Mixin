using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    public PickupLocation pickupLocation;
    [Header("Between 0.0 and 1.0")]
    public float stealRate;

    public bool HasPickedUp()
    {
        return pickupLocation.HasPickedUp;
    }
}
