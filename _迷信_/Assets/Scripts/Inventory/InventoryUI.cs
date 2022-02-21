using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // Set up the UI layout of inventory panel
    // Switch to Grid layout later?
    public int X_SPACE_BETWEEN_SLOT;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_SLOT;

    //Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
    }

    private void Start()
    {
         inventory.OnItemListChanged += OnInventoryChanged;
    }

    void OnInventoryChanged(object sender, EventArgs e)
    {
        RefreshDisplay();
    }

    private void Update()
    {
        //UpdateDisplay();
    }

    private void OnEnable()
    {
        RefreshDisplay();
    }

    private void OnDisable()
    {
        DestroyDisplay();
    }

    private void DestroyDisplay()
    {
        foreach (Transform child in itemSlotContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void RefreshDisplay()
    {
        for (int i = 0; i < inventory.Container.ItemList.Count; i++)
        {
            InventorySlot slot = inventory.Container.ItemList[i];
            var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, itemSlotContainer);
            obj.transform.Find("image").GetComponent<Image>().sprite = inventory.database.GetItem[slot.item.Id].sprite;
            obj.GetComponent<RectTransform>().localPosition = GetSlotPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount > 1 ? slot.amount.ToString() : "";
            //itemsDisplayed.Add(slot, obj);
        }
    }

    //public void UpdateDisplay()
    //{
    //    for (int i = 0; i < inventory.Container.ItemList.Count; i++)
    //    {
    //        InventorySlot slot = inventory.Container.ItemList[i];

    //        if (itemsDisplayed.ContainsKey(slot))
    //        {
    //            itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().SetText(slot.amount.ToString("n0"));
    //        }
    //        else
    //        {
    //            var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, itemSlotContainer);
    //            obj.transform.Find("image").GetComponent<Image>().sprite = inventory.database.GetItem[slot.item.Id].sprite;
    //            obj.GetComponent<RectTransform>().localPosition = GetSlotPosition(i);
    //            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount > 1 ? slot.amount.ToString() : "";
    //            itemsDisplayed.Add(slot, obj);
    //        }
    //    }
    //}

    public Vector3 GetSlotPosition(int i)
    {
        return new Vector3(X_SPACE_BETWEEN_SLOT * (i % NUMBER_OF_COLUMN),
                            (-Y_SPACE_BETWEEN_SLOT * (i / NUMBER_OF_COLUMN)),
                            0f);
    }
}
