using System;
using UnityEngine;

public abstract class RuntimeAnchorSO<T> : ScriptableObject where T : class
{
	private T _value;

	public bool IsSet { get; private set; }

	public T Value
	{
		get { return _value; }
		set
		{
			_value = value;
			IsSet = _value != null;
		}
	}

	public void OnDisable()
	{
		_value = null;
		IsSet = false;
	}
}
