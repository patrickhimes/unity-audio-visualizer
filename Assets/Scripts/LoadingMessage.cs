using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingMessage : MonoBehaviour {
	public float blinkRate = 0.1f;
	public AudioSource audio;

	private Text textUI;
	private Color color1;
	private Color color2;

	// Use this for initialization
	void Start () {
		textUI = GetComponent<Text> ();
		color1 = textUI.color;
		color2 = new Color (textUI.color.r, textUI.color.g, textUI.color.b, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!audio.isPlaying) {
			textUI.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time * blinkRate, 1.0f));
		} else {
			textUI.color = Color.Lerp(textUI.color, color2, Time.time * blinkRate);
			if (textUI.color == color2) {
				Destroy (this.gameObject);
			}
		}	
	}
}
