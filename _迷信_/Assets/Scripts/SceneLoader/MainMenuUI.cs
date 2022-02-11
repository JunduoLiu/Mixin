using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MainMenuUI : MonoBehaviour
{

    private void Awake()
    {
        transform.Find("start_btn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Debug.Log("Start Game...");
            Loader.Load(enumScene.HouseShenXin);
        };

    }
}