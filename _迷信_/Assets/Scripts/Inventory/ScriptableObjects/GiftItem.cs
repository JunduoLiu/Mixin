using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Gift Item", menuName = "InventorySystem/Items/Gift")]
public class GiftItem : Item
{
    public string giftFrom;

    public void Awake()
    {
        itemType = ItemType.Gift;
    }

    new public string ItemDescription()
    {
        return String.Format("{0}送的{1}", giftFrom, itemDesc);
    }
}
