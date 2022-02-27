using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;
using CodeMonkey.Utils;

public class SaveSystemUI : MonoBehaviour
{
    public GameObject prefab;
    public int slotNumber;
    private Transform saveSlotContainer;

    Dictionary<GameObject, int> savedGameSlots = new Dictionary<GameObject, int>();

    void Awake()
    {
        saveSlotContainer = transform.Find("saveSlotContainer");
        transform.Find("SaveLoadMenu").GetComponent<Button_UI>().ClickFunc = () => {
                SaveSystem.SaveToSlot(1);
        };
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
