using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
class TextBlinking : MonoBehaviour
{
	[SerializeField]
	private float blinkDuration = 0f;
	[SerializeField]
	private float blinkSpeed = 5f;
	[SerializeField]
	private float decreaseFactor = 1.0f;

	private Color color;
	private Color startColor;
	float alpha;
	Text text;

    void Start ()
    {
		text = GetComponent<Text> ();
		color = text.color;
		startColor = color;
		alpha = startColor.a;
    }

	void OnEnable()
	{
		//startColor = color;
	}
 
	void Update ()
	{
		if (blinkDuration > 0) 
		{
			while (true)
			{
				color.a = Mathf.MoveTowards(color.a, alpha, Time.deltaTime * blinkSpeed);
				text.color = color;

				if (color.a  == startColor.a)
				{
						alpha = 0.0f;
				} 
				else if (color.a  == 0)
				{
						alpha = startColor.a;
					}
				blinkDuration -= Time.deltaTime * decreaseFactor;
				return;
			}
		}
		else
		{
			blinkDuration = 0;
			color = startColor;
			text.color = color;
		}
	}

	public void Blink (float duration)
	{
		blinkDuration = duration;
	}
	public void Blink (float duration, float spead)
	{
		blinkDuration = duration;
		blinkSpeed = spead;
	}
}