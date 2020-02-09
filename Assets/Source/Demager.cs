using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demager : MonoBehaviour 
{
	public int value = 1;

	void OnCollisionEnter2D(Collision2D coll)
	{
		coll.gameObject.SendMessage ("SetDemage", value);
	}
}
