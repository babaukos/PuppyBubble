using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScore : MonoBehaviour 
{
	public int scoreMultiplay = 10;
	// score timer
	private float timerTime = 0;
	private float memoryTime = 0;
	
	// Use this for initialization
	void Start () 
	{
		memoryTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameManager.Instance == null)
			return;

		if(GameManager.Instance.pause != true)
		{
			GameScoreTimer();
		}
	}
	private void GameScoreTimer()
	{
		timerTime = Time.time - memoryTime;
		GameManager.Instance.Setscore((int)timerTime * scoreMultiplay);
	}
	private void OnEnable()
	{
		memoryTime = Time.time;
	}

}
