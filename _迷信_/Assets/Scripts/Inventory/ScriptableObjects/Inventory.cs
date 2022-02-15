using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "InventorySystem/Inventory")]
// public class Inventory : ScriptableObject, ISerializationCallbackReceiver
public class Inventory : ScriptableObject
{
    public string savePath;
    public ItemDatabase database;
    public InventoryContainer Container;
    public event EventHandler OnItemListChanged;

    // private void OnEnable()
    // {
    // #if UNITY_EDITOR
    //     database = (ItemDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database/PlayerItemDatabase.asset", typeof(ItemDatabase));
    // #else
    //     database = Resources.Load<ItemDatabase>("PlayerItemDatabase");
    // #endif
    // }

    public void AddItem(Item _item, int _amount)
    {
        for (int i = 0; i < Container.ItemList.Length; i++)
        {
            if(Container.ItemList[i].item == _item)
            {
                Container.ItemList[i].AddAmount(_amount);
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
        SetFirstEmptySlot(_item, _amount);

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item _item, int _amount)
    {
        Debug.Log("Remove Item. Implementing...");
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public InventorySlot SetFirstEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.ItemList.Length; i++)
        {
            if (Container.ItemList[i].ID <= -1)
            {
                Container.ItemList[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.ItemList[i];
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
    //     formatter.Serialize(stream, Container);
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
    //         Container = (InventoryContainer)formatter.Deserialize(stream);
    //         stream.Close();
    //     }
    // }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new InventoryContainer();
    }

    // As soon as any changes are made to the ScriptableObject,
    // repopulate the item slot, so that items match the ids
    // public void OnAfterDeserialize()
    // {
    //     for (int i = 0; i < Container.ItemList.Count; i++)
    //         Container.ItemList[i].item = database.GetItem[Container.ItemList[i].ID];
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
    public InventorySlot[] ItemList = new InventorySlot[24];
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public int amount;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }


    public void AddAmount(int value)
    {
        amount += value;
    }

    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
}

// public class InventoryItem
// {
//     pub
// }
