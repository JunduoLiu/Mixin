using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Item", menuName = "InventorySystem/Items/Default")]
public class DefaultItem : Item
{
    public void Awake()
    {
        itemType = ItemType.Default;
    }
}
