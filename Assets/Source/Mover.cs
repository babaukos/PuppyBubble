using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public float speed = 1;

	// Update is called once per frame
	void Update () 
	{
		if (GameManager.Instance == null)
			return;

		float sp;
		if (!GameManager.Instance.IsPause()) 
		{
			sp =  PlayerControl.Instance.GetSpeed () / 310 + (speed/200);
		} 
		else 
		{
			sp = 0;
		}
		transform.position += (Vector3.left * sp);
	}
}
