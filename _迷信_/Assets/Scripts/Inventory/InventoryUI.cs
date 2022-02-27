using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using CodeMonkey.Utils;
using TMPro;
using System;

/*
 * Set up Inventory UI
 * */

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject prefab;
    private Transform itemSlotContainer;
    private Transform itemInfo;
    // private GameObject selectedSlot;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemInfo = transform.Find("itemInfo");
        // selectedSlot = null;
    }

    private void Start()
    {
        inventory.InventoryUpdated.AddListener(OnInventoryUpdated);

        DestroyDisplay();
        CreateDisplay();
    }

    private void OnEnable()
    {
        itemInfo.gameObject.SetActive(false);
    }

    private void OnInventoryUpdated(InventorySlot _slot)
    {
        foreach (Transform child in itemSlotContainer)
        {
            GameObject obj = child.gameObject;
            if (itemsDisplayed[obj].id == _slot.id)
            {
                obj.GetComponentInChildren<InventorySlotUI>().SetInventorySlotUI(inventory.database, _slot);
                if (_slot.id == -1)
                {
                    itemsDisplayed.Remove(obj);
                }
                return;
            }
        }
        var newObj = Instantiate(prefab, Vector3.zero, Quaternion.identity, itemSlotContainer);
        newObj.GetComponentInChildren<InventorySlotUI>().SetInventorySlotUI(inventory.database, _slot);
        newObj.GetComponentInChildren<InventorySlotUI>().InventorySlotSelected.AddListener(OnInventorySlotSelected);
        itemsDisplayed.Add(newObj, _slot);
    }

    private void OnInventorySlotSelected(GameObject obj)
    {
        InventorySlot slot = itemsDisplayed[obj];
        Item item = inventory.database.GetItem[slot.id];
        Debug.Log(item.itemName + " 被选中了");

        transform.Find("itemInfo/itemDesc/itemDescText").GetComponent<Text>().text = item.ItemDescription();
        transform.Find("itemInfo/itemDesc/itemNameText").GetComponent<Text>().text = item.itemName;
        transform.Find("itemInfo/itemImage/image").GetComponent<Image>().sprite = item.sprite;
        transform.Find("itemInfo/ReturnBtn").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Start return mechanism, to be implemented...");
        };
        itemInfo.gameObject.SetActive(true);
    }

    private void DestroyDisplay()
    {
        InventorySlot emptySlot = new InventorySlot(-1, -1);
        foreach (Transform child in itemSlotContainer.transform)
        {
            child.GetComponentInChildren<InventorySlotUI>().SetInventorySlotUI(inventory.database, emptySlot);
        }
        itemsDisplayed.Clear();
    }

    private void CreateDisplay()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.container.ItemList.Length; i++)
        {
            InventorySlot slot = inventory.container.ItemList[i];
            var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, itemSlotContainer);
            obj.GetComponentInChildren<InventorySlotUI>().SetInventorySlotUI(inventory.database, slot);
            if (slot.id > -1)
            {
                itemsDisplayed.Add(obj, slot);

                obj.GetComponentInChildren<InventorySlotUI>().InventorySlotSelected.AddListener(OnInventorySlotSelected);
            }
        }
    }

}
