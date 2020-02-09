using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScore : MonoBehaviour 
{
	public string tagActivator = "Player";
	public int scoreValue = 1;

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (GameManager.Instance == null)
			return;

		if(other.tag == tagActivator)
		{
			GameManager.Instance.SetscoreInc(scoreValue);
		}
	}
}
