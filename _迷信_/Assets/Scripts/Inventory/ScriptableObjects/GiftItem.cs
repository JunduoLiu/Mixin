using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gift Item", menuName = "InventorySystem/Items/Gift")]
public class GiftItem : Item
{
    public void Awake()
    {
        itemType = ItemType.Gift;
    }
}
