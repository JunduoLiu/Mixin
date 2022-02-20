using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Int Variable", menuName = "Variable/Int")]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public int InitialValue;

	[System.NonSerialized]
	public int RuntimeValue;

	public UnityEvent IntVariableChanged;
	public UnityEvent IntVariableReachedZero;

	void OnStart()
    {
        if (IntVariableChanged == null)
        {
            IntVariableChanged = new UnityEvent();
        }

        if (IntVariableReachedZero == null)
        {
            IntVariableReachedZero = new UnityEvent();
        }
    }

	public void OnAfterDeserialize()
	{
		RuntimeValue = InitialValue;
	}

	public void OnBeforeSerialize() {
		// maybe? -hazel
		// 可能是为了避免存储的value在runtime被messup？
		InitialValue = RuntimeValue;
	}

	public void AddValue(int _value)
    {
		RuntimeValue = RuntimeValue + _value < 100 ? RuntimeValue + _value : 100;
		IntVariableChanged.Invoke();
	}

	public void ReduceValue(int _value)
	{
		RuntimeValue = RuntimeValue > _value ? RuntimeValue - _value : 0;

		if (RuntimeValue == 0)
		{
			IntVariableReachedZero.Invoke();
		}

		IntVariableChanged.Invoke();
	}

}
