using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using CodeMonkey.Utils;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public InventoryUIEvent InventorySlotSelected;
    public int id;

    void Awake()
    {
        if (InventorySlotSelected == null)
        {
            InventorySlotSelected = new InventoryUIEvent();
        }
        transform.GetComponent<Button_UI>().ClickFunc = () => {
                InventorySlotSelected.Invoke(gameObject);
        };
    }

    public void SetInventorySlotUI (ItemDatabase _db, InventorySlot _slot)
    {
        if (_slot.id > -1)
        {
            id = _slot.id;
            transform.Find("image").GetComponent<Image>().sprite = _db.GetItem[id].sprite;
            transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount > 1 ? _slot.amount.ToString() : "";
        } else {
            id = -1;
            transform.Find("image").gameObject.SetActive(false);
            transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }


}

[System.Serializable]
public class InventoryUIEvent : UnityEvent<GameObject>
{
}
