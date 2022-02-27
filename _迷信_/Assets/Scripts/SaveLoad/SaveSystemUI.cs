using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PixelCrushers;
using CodeMonkey.Utils;

public class SaveSystemUI : MonoBehaviour
{
    public GameObject prefab;
    private Transform saveSystemPanel;
    private Transform saveSlotContainer;
    private SaveEntryContainer container;

    Dictionary<GameObject, SaveEntry> entriesDisplayed = new Dictionary<GameObject, SaveEntry>();

    void Awake()
    {
        if (container == null)
        {
            container = new SaveEntryContainer();
        }

        saveSystemPanel = transform.Find("saveSystemPanel");
        saveSlotContainer = transform.Find("saveSlotContainer");
        transform.Find("saveLoadMenu").GetComponent<Button_UI>().ClickFunc = () => {
                saveSystemPanel.gameObject.SetActive(!saveSystemPanel.gameObject.activeSelf);
        };
        transform.Find("saveBtn").GetComponent<Button_UI>().ClickFunc = () => {
                SaveGame();
        };
    }

    void Start()
    {
        CreateDisplay();
    }

    private void SaveGame()
    {
        int slotIndex = SetFirstEmptySlot(SceneManager.GetActiveScene().buildIndex, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        if (slotIndex > -1)
        {
            SaveSystem.SaveToSlot(slotIndex);
            RefreshDisplay();
        } else {
            // alert full save slot
        }
    }

    public int SetFirstEmptySlot(int _sceneIndex, string _time)
    {
        for (int i = 0; i < container.EntryList.Length; i++)
        {
            if (container.EntryList[i].sceneIndex <= -1)
            {
                container.EntryList[i].UpdateEntry(_sceneIndex, _time);
                return i;
            }
        }
        return -1; // save slot is full
    }

    private void CreateDisplay()
    {
        entriesDisplayed = new Dictionary<GameObject, SaveEntry>();
        for (int i = 0; i < container.EntryList.Length; i++)
        {
            SaveEntry entry = container.EntryList[i];
            var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, saveSlotContainer);
            obj.transform.Find("dateTimeText").GetComponent<Text>().text = entry.time;
            obj.transform.Find("sceneText").GetComponent<Text>().text = entry.sceneIndex > -1 ? SceneManager.GetSceneByBuildIndex(entry.sceneIndex).name : "";
            if (entry.sceneIndex > -1)
            {
                entriesDisplayed.Add(obj, entry);
            }
        }
    }

    private void RefreshDisplay()
    {

    }

    private void OnSaveEntrySelected()
    {

    }
}

[System.Serializable]
public class SaveEntryContainer
{
    public SaveEntry[] EntryList;

    public SaveEntryContainer()
    {
        EntryList = new SaveEntry[6];
        for (int i = 0; i < EntryList.Length; i++) {
            EntryList[i] = new SaveEntry();
        }
    }
}

[System.Serializable]
public class SaveEntry
{
    public int sceneIndex;
    public string time; // DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")

    public SaveEntry()
    {
        sceneIndex = -1;
        time = "";
    }

    public SaveEntry(int _sceneIndex, string _time)
    {
        sceneIndex = _sceneIndex;
        time = _time;
    }

    public void UpdateEntry(int _sceneIndex, string _time)
    {
        sceneIndex = _sceneIndex;
        time = _time;
    }
}
