using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerControl : MonoBehaviour 
{
	public int life = 1;
	[SerializeField]
	private float jampHight = 1;
	[SerializeField]
	private float runSpeed = 1;
	[SerializeField]
	private float rayLangh = 1;
	[SerializeField]
	private AudioClip jampSound;
	[SerializeField]
	private AudioClip hitSound;
	[SerializeField]
	public AudioClip deadSound;

	private Rigidbody2D rigidbody;
	private Animator animator;
	private AudioSource source;
	private BoxCollider2D collider;

	public ParticleSystem groundPart; 
	public Transform shadow;

	public static PlayerControl Instance;
	private CameraShake cameraShake;

	private bool isDead;

	// Use this for initialization
	private void Start () 
	{
		if (Instance == null) 
		{
			Instance = this; 
		} else if(Instance == this)
		{
			Destroy(gameObject); 
		}

		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
		collider = GetComponent<BoxCollider2D> ();

		cameraShake = Camera.main.GetComponent<CameraShake>();
	}	
	// Update is called once per frame
	private void Update () 
	{
		if (GameManager.Instance == null)
			return;

		if (life <= 0 && isDead == false) 
		{
				Handheld.Vibrate ();
				source.PlayOneShot (deadSound);
				GameManager.Instance.GameOver();
				isDead = true;
		} 
		else 
		{
			if(!GameManager.Instance.pause)
			{
				GroundEffect ();
				if (IsGrounded () && (Input.GetKeyDown (KeyCode.Space)||Input.GetMouseButtonDown(0)))
				{
					rigidbody.AddForce (Vector2.up * jampHight, ForceMode2D.Impulse);
					animator.SetBool ("jump", true);

					if (jampSound != null) 
					{
						source.PlayOneShot (jampSound);
						Handheld.Vibrate ();
					}
				} 
				else 
				{
					animator.SetBool ("jump", false);
				}
				//rotObj.transform.eulerAngles = new Vector3 (0, 0, rigidbody.velocity.y * dirMultipl);
			}
		}
	}
	//
	private void GroundEffect()
	{
		// Еффект тени и пыли
		RaycastHit2D rh = Physics2D.Raycast(transform.position, Vector3.down, 2);
		if (rh)
		{
			shadow.gameObject.SetActive(true);
			shadow.position = rh.point;
			groundPart.transform.position =  rh.point;

			float sc = Mathf.Clamp01(1 / (rh.distance * 3));
			shadow.localScale = new Vector3(sc, sc, 1);
		}
		else 
		{
			shadow.gameObject.SetActive(false);
		}
	}
	//
	private void SetDemage(int arg)
	{
		life -= arg;
	}
	//
	private bool IsGrounded()
	{
		Debug.DrawRay (transform.position, -transform.up * rayLangh, Color.red);
		if (Physics2D.Raycast (transform.position, -transform.up, rayLangh))
		{
			return true;
		} 
		else 
		{
			return false;
		}
	}
	//
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (IsGrounded () == true) 
		{
			if (hitSound != null)
				source.PlayOneShot (hitSound);

			if (groundPart.isPlaying == false)
				groundPart.Play ();

//			if (GameManager.Instance.IsGame ())
//			    cameraShake.Shake (0.09f);
		}
	}
	//
	public float GetSpeed()
	{
		return runSpeed;
	}
	public void SetSpeed( float s)
	{
		runSpeed = s;
	}
}
