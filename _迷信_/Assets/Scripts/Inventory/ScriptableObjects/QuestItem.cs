using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Item", menuName = "InventorySystem/Items/Quest")]
public class QuestItem : Item
{
    public void Awake()
    {
        itemType = ItemType.Quest;
    }
}
