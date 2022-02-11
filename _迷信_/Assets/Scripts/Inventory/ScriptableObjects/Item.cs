using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Default, // Only for Testing Purposes
    Stolen,
    Quest,
    Gift,
}

public abstract class Item : ScriptableObject
{
    public Sprite sprite;
    public int Id; // What does this do eventually?

    public ItemType itemType;
    public string itemName;
    [TextArea(15,20)]
    public string itemDesc;

    // Not sure if necessary
    // public bool IsStackable()
    // {
    //     switch (itemType)
    //     {
    //         default:
    //         case ItemType.Stolen:   return false;
    //         case ItemType.Quest:    return true;
    //     }
    // }

}
