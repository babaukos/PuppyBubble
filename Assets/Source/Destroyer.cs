using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	[SerializeField]
	private float time = 5.0f;

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
