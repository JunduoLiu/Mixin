using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class PlayerStatus : MonoBehaviour
{
    public Slider slider;

    public int MAX_VALUE;

    public IntVariable statusValue;

    public void UpdateValue()
    {
        slider.value = statusValue.RuntimeValue;
    }

    private void Awake()
    {
        slider.maxValue = MAX_VALUE;
        slider.value = statusValue.RuntimeValue;
    }

    private void Start()
    {
        statusValue.IntVariableChanged.AddListener(OnValueChanged);
    }


    void OnValueChanged()
    {
        UpdateValue();
    }


}
