using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeybordEvent : MonoBehaviour
{
	public KeyCode key;
	public UnityEvent Event;

	void Update ()
	{
		if(Input.GetKeyDown(key))
		{
			Event.Invoke();
		}
	}
}
