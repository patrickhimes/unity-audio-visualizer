using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InputSelect : MonoBehaviour {

	public Dropdown inputSelect;
	public MicrophoneInput microphone;
	public StreamingStation music;
	public AudioVisualizer audioVizualizer;

	private List<string> options = new List<string>();

	// Use this for initialization
	void Start () {

		options.Add("Music");
		options.Add("Microphone");
	
		inputSelect.AddOptions(options);

		inputSelect.onValueChanged.AddListener(delegate {
			InputSelectValueChangedHandler(inputSelect);
		});
	}

	public void InputSelectValueChangedHandler(Dropdown mic){
		string selectedInput = options[inputSelect.value];

		switch (selectedInput)
		{
		case "Music":
			audioVizualizer.audioSource = music.GetAudioSource();
			music.Play ();
			microphone.Stop ();
			break;
		case "Microphone":
			audioVizualizer.audioSource = microphone.GetAudioSource();
			music.Pause ();
			microphone.Play ();
			break;
		}
	}
}
