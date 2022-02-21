using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stolen Item", menuName = "InventorySystem/Items/Stolen")]
public class StolenItem : Item
{
    public enumScene stolenScene;

    public void Awake()
    {
        itemType = ItemType.Stolen;
    }
}
