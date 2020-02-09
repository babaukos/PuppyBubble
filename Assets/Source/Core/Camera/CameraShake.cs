using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;	
	[SerializeField]
	private float shakeDuration = 0f;
	[SerializeField]
	private float shakeAmount = 0.7f;
	[SerializeField]
	private float decreaseFactor = 1.0f;
	
	Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent<Transform>();
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			var offset = originalPos + Random.insideUnitSphere * shakeAmount;

			camTransform.localPosition = new Vector3 (offset.x, offset.y, camTransform.localPosition.z);
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}

	public void Shake (float duration)
	{
		shakeDuration = duration;
	}
}
