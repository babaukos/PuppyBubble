using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	[Space]
	public Transform player;
	[Space]
	public Renderer back;
	//public float backSpeed = 1;
	[Space]
	public Renderer cloud;
	public float cloudSpeed = 0.5f;
	[Space]
	public Renderer ground;
	public float groundSpeed = 1;
	[Space]
	public Renderer front;
	public float frontSpeed = 2;
	[Space]
	public Renderer face;
	public float faceSpeed = 3;
	[Space]
	public Transform[] spawnLines;
	public Obstacles[] obstacles;
	[System.Serializable]
	public class Obstacles
	{
		public GameObject obstacl;
		[Tooltip("начало спавна ")]
		public float startSpawnTime = 0;
		[Tooltip("один спавн / spawRate")]
		public float spawRate = 100;
		[Tooltip("конец спавна (продолжительность = начало спавна + конец спавна)")]
		public float endSpawnTime = 10;
		[Tooltip("wear spawning")]
		public int numberSpawnlin;
		[HideInInspector]
		public float timer;
		public float offset;
	}
	[Space]
	public AnimationCurve difficult;
	private GameObject dinamicObject;
	public static LevelManager Instance;
	private List<GameObject> obstaclesPull;
	// game timer
	private float timerTime;
	private float memoryTime;

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
		player.position = Vector3.zero;
		dinamicObject = GameObject.Find ("[DinamicObjects]");
		memoryTime = Time.time;
	}
	
	// Update is called once per frame
	private void Update () 
	{
		if (GameManager.Instance == null)
			return;
		
		timerTime = Time.time - memoryTime;
		// Spawning Tube
		if (!GameManager.Instance.IsPause()) 
		{
			for (int spObj = 0; spObj < obstacles.Length; spObj++) 
			{
				if(timerTime >= obstacles [spObj].startSpawnTime 
				&& timerTime <= obstacles [spObj].startSpawnTime + obstacles [spObj].endSpawnTime)
				{
					if (obstacles [spObj].timer >= obstacles [spObj].spawRate)
					{
						obstacles [spObj].timer = 0;
						float ofs = Random.Range(-obstacles[spObj].offset, obstacles[spObj].offset);
						Vector3 posOfst = new Vector3 (spawnLines[obstacles[spObj].numberSpawnlin].position.x,spawnLines[obstacles[spObj].numberSpawnlin].position.y + ofs, spawnLines[obstacles[spObj].numberSpawnlin].position.z);
						Quaternion rot = obstacles[spObj].obstacl.transform.rotation;

						GameObject obst = (GameObject)Instantiate (obstacles [spObj].obstacl, posOfst, rot);
						obst.transform.parent = dinamicObject.transform;
					} 
					else
					{
						obstacles [spObj].timer += 1;
					}
				}
			}

			float div = 50;
			Vector2 cloTextOffset = new Vector2(Time.time * (PlayerControl.Instance.GetSpeed()/div * cloudSpeed), 0);
			cloud.material.mainTextureOffset = cloTextOffset;

			Vector2 grTextOffset = new Vector2(Time.time * (PlayerControl.Instance.GetSpeed()/div * groundSpeed), 0);
			ground.material.mainTextureOffset = grTextOffset;

			Vector2 frnTextOffset = new Vector2(Time.time * (PlayerControl.Instance.GetSpeed()/div * frontSpeed), 0);
			front.material.mainTextureOffset = frnTextOffset;

			Vector2 faceTextOffset = new Vector2(Time.time * (PlayerControl.Instance.GetSpeed()/div * faceSpeed), 0);
			face.material.mainTextureOffset = faceTextOffset;

			// if (PlayerControl.Instance != null)
			// 	PlayerControl.Instance.SetSpeed (difficult.Evaluate(GameManager.Instance.GetTime()));
		}
	}
	//
	private void OnValidate()
	{
		SetSizeObjectsSnene();
	}
	//
	private void SetSizeObjectsSnene()
	{
		if (Camera.main == null)
			return;
		
		var camSize = Camera.main.orthographicSize;
		var hightBG = camSize * 2;
		var widthBG = hightBG * Screen.width / Screen.height;

		back.transform.localScale = new Vector3 (widthBG, hightBG, 0);

		cloud.transform.localScale = new Vector3 (widthBG, 1f, 0);

		ground.transform.localScale = new Vector3 (widthBG, 1f, 0);

		front.transform.localScale = new Vector3 (widthBG, 1f, 0);

		face.transform.localScale = new Vector3 (widthBG, 1f, 0);
	}
	//
	private void OnDrawGizmos()
	{
		for(int i=0; i < spawnLines.Length; i++)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawRay (spawnLines [i].position, Vector3.left * 5);
		}
		#if UNITY_EDITOR
		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.blue; 

		UnityEditor.Handles.Label(back.transform.position, back.material.mainTextureOffset.x.ToString(), style);
		UnityEditor.Handles.Label(ground.transform.position, ground.material.mainTextureOffset.x.ToString(), style);
		UnityEditor.Handles.Label(face.transform.position, face.material.mainTextureOffset.x.ToString(), style);
		#endif
	}
}
