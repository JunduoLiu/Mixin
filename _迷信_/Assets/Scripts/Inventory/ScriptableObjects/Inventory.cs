using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "InventorySystem/Inventory")]
// public class Inventory : ScriptableObject, ISerializationCallbackReceiver
public class Inventory : ScriptableObject
{
    public string savePath;
    public ItemDatabase database;
    public InventoryContainer container;
    public InventoryUpdateEvent InventoryUpdated;

    // void OnEnable()
    // {
    // #if UNITY_EDITOR
    //     database = (ItemDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database/PlayerItemDatabase.asset", typeof(ItemDatabase));
    // #else
    //     database = Resources.Load<ItemDatabase>("PlayerItemDatabase");
    // #endif
    // }

    void Awake()
    {
        if (InventoryUpdated == null)
        {
            InventoryUpdated = new InventoryUpdateEvent();
        }
    }

    void Start()
    {

    }

    public void AddItem(Item _item, int _amount)
    {
        for (int i = 0; i < container.ItemList.Length; i++)
        {
            if(container.ItemList[i].id == _item.id)
            {
                container.ItemList[i].amount += _amount;
                InventoryUpdated.Invoke(container.ItemList[i]);
                return;
            }
        }
        InventorySlot firstEmptySlot = SetFirstEmptySlot(_item, _amount);
        if (firstEmptySlot != null)
        {
            InventoryUpdated.Invoke(firstEmptySlot);
        } else {
            // alert full inventory
        }

    }

    public void RemoveItem(Item _item, int _amount)
    {
        Debug.Log("Remove Item. Implementing...");
        for (int i = 0; i < container.ItemList.Length; i++)
        {
            if(container.ItemList[i].id == _item.id)
            {
                container.ItemList[i].amount -= _amount;
                if (container.ItemList[i].amount == 0)
                {
                    container.ItemList[i].id = -1;
                }
                InventoryUpdated.Invoke(container.ItemList[i]);
                return;
            }
        }
        // ItemRemoved.Invoke(slot, _amount);
    }

    public InventorySlot SetFirstEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < container.ItemList.Length; i++)
        {
            if (container.ItemList[i].id <= -1)
            {
                container.ItemList[i].UpdateSlot(_item.id, _amount);
                return container.ItemList[i];
            }
        }
        return null; // inventory is full
    }

    // Don't need to save Scriptable Objects in runtime

    // 在Editor里选中Inventory Asset后，点击Inspector里的齿轮图标-Save可以手动保存
    // [ContextMenu("Save")]
    // public void Save()
    // {
    //     /* 这样的话玩家可以自行修改本地Json文件
    //     // string saveData = JsonUtility.ToJson(this, true);
    //     // BinaryFormatter bf = new BinaryFormatter();
    //     // FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
    //     // bf.Serialize(file, saveData);
    //     // file.Close();
    //     */
    //
    //     IFormatter formatter = new BinaryFormatter();
    //     Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
    //     formatter.Serialize(stream, container);
    //     stream.Close();
    // }
    //
    // [ContextMenu("Load")]
    // public void Load()
    // {
    //     if(File.Exists(string.Concat(Application.persistentDataPath, savePath))){
    //         /* 这样的话玩家可以自行修改Json文件
    //         BinaryFormatter bf = new BinaryFormatter();
    //         FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
    //         JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
    //         file.Close();
    //         */
    //
    //         IFormatter formatter = new BinaryFormatter();
    //         Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
    //         container = (Inventorycontainer)formatter.Deserialize(stream);
    //         stream.Close();
    //     }
    // }

    [ContextMenu("Clear")]
    public void Clear()
    {
        container = new InventoryContainer();
    }

    // As soon as any changes are made to the ScriptableObject,
    // repopulate the item slot, so that items match the ids
    // public void OnAfterDeserialize()
    // {
    //     for (int i = 0; i < container.ItemList.Count; i++)
    //         container.ItemList[i].item = database.GetItem[container.ItemList[i].id];
    // }
    //
    // public void OnBeforeSerialize()
    // {
    //     // Required method. But we won't need this.
    // }

}

[System.Serializable]
public class InventoryContainer
{
    public InventorySlot[] ItemList;

    public InventoryContainer()
    {
        ItemList = new InventorySlot[24];
    }
}

[System.Serializable]
public class InventorySlot
{
    public int id;
    public int amount;

    public InventorySlot()
    {
        id = -1;
        amount = 0;
    }
    public InventorySlot(int _id, int _amount)
    {
        id = _id;
        amount = _amount;
    }

    public void UpdateSlot(int _id, int _amount)
    {
        id = _id;
        amount = _amount;
    }
}

[System.Serializable]
public class InventoryUpdateEvent : UnityEvent<InventorySlot>
{
}
