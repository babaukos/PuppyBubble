using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	[Header("	Panels Section")]
	public RectTransform menuPanel;
	public RectTransform gameOverPanel;
	public RectTransform scorePanel;
	public RectTransform pausePanel;
	public RectTransform waitingBlockPanel;
	[Header("	Game Counter Section")]
	public Text ScoreText;
	public Text HIScoreText;
	public AudioClip scoreRangSound;
	public Button menuButton;
	//
	public bool pause = true;
	//
	public bool censure = true;
	//
	private int score;
	private int HIscore;
	//
	public static GameManager Instance;
	private AudioSource source;

	// Initialization
	private void Awake()
	{
		if (Instance == null) 
		{
			Instance = this; 
		} 
		else
		{
			Destroy(gameObject); 
		}

	}
	// Use this for initialization
	private void Start ()
	{
		source = GetComponent<AudioSource>();
		DontDestroyOnLoad(this); 
	}
	// Update is called once per frame
	private void Update () 
	{
		if(!pause)
		{
			ScoreEffect();
			IntarfaceUpdate ();
		}
	}
	//
	private void IntarfaceUpdate()
	{
		if (ScoreText != null)
			ScoreText.text = score.ToString();
		if (HIScoreText != null)
			HIScoreText.text = HIscore.ToString ();
	}
	//
	public bool IsPause()
	{
		return pause;
	}
	//
	private void AddScore()
	{
		if(score > HIscore)
		{
			HIscore = score;
			score =	0;
		}
		else
		{
			score =	0;
		}
	}
	private void ScoreEffect()
	{
		if ((score != 0) && (score % 100) == 0) 
		{
			ScoreText.GetComponent<TextBlinking> ().Blink (1);
			if (!source.isPlaying)
			source.PlayOneShot (scoreRangSound);
		} 
	}
	// Запускаем игру
	public void GameStart()
	{
		pause = true;
		Time.timeScale = 0;
		SceneManager.LoadScene(1);

		menuPanel.gameObject.SetActive(false);
		gameOverPanel.gameObject.SetActive (false);

		//menuButton.gameObject.SetActive(false);
		scorePanel.gameObject.SetActive (true);
		waitingBlockPanel.gameObject.SetActive(true);
	}
	// Запускаем уровень
	public void StartLevel()
	{
	    pause = false;
		Time.timeScale = 1;

		menuPanel.gameObject.SetActive(false);
		gameOverPanel.gameObject.SetActive (false);

		scorePanel.gameObject.SetActive (true);
		//menuButton.gameObject.SetActive(true);
		waitingBlockPanel.gameObject.SetActive(false);
	}
	// Выход из игры
	public void GameExit()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
	//  Поставить на паузу
	public void GamePause(bool arg)
	{
		pause = arg;
		if(arg == true)
		{
			Time.timeScale = 0;
			menuButton.gameObject.SetActive(false);
		}
		else
		{
			Time.timeScale = 1;
			menuButton.gameObject.SetActive(true);
		}

		// menuPanel.gameObject.SetActive(!arg);
		// scorePanel.gameObject.SetActive (arg);
		// resetPanel.gameObject.SetActive (!arg);
		pausePanel.gameObject.SetActive (arg);
	}
	//  Поставить на паузу
	public void GameOver()
	{
		AddScore();
		pause = true;
		Time.timeScale = 0;

		scorePanel.gameObject.SetActive (true);
		gameOverPanel.gameObject.SetActive (true);

		menuPanel.gameObject.SetActive (false);
		pausePanel.gameObject.SetActive (false);
		//menuButton.gameObject.SetActive (false);
	}
	// GameRestart
	public void GameRestart()
	{
		GameStart();

	    // menuPanel.gameObject.SetActive(false);
		// scorePanel.gameObject.SetActive (true);
		// resetPanel.gameObject.SetActive (false);
		// pausePanel.gameObject.SetActive (false);
		// menuButton.gameObject.SetActive (true);
	}
	// В главное меню
	public void GameToMain()
	{
		SceneManager.LoadScene(0);

		menuPanel.gameObject.SetActive(true);
		scorePanel.gameObject.SetActive (false);
		gameOverPanel.gameObject.SetActive (false);
		pausePanel.gameObject.SetActive (false);

		//menuButton.gameObject.SetActive(false);
		waitingBlockPanel.gameObject.SetActive(false);
	}
	public void GameCensure(bool arg)
	{
		censure = arg;
	}
	public void Setscore(int arg)
	{
		score = arg;
	}
	public void SetscoreInc(int arg)
	{
		score += arg;
	}
	public int GetScore()
	{
		return score;
	}
}
