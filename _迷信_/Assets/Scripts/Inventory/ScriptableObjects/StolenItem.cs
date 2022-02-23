using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Stolen Item", menuName = "InventorySystem/Items/Stolen")]
public class StolenItem : Item
{
    public enumScene stolenScene;

    public void Awake()
    {
        itemType = ItemType.Stolen;
    }

    new public string ItemDescription()
    {
        return String.Format("从{0}偷来的{1}", stolenScene.ToString(), itemDesc);
    }
}
