using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Int Variable", menuName = "Variable/Int")]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public int InitialValue;

	[System.NonSerialized]
	public int RuntimeValue;

	public event EventHandler IntVariableChanged;
	public event EventHandler IntVariableReachedZero;

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
		IntVariableChanged(this, EventArgs.Empty);
	}

	public void ReduceValue(int _value)
	{
		RuntimeValue = RuntimeValue > _value ? RuntimeValue - _value : 0;
		if (RuntimeValue == 0) { IntVariableReachedZero(this, EventArgs.Empty); }
		IntVariableChanged(this, EventArgs.Empty);
	}

}
