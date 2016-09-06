using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {
	public float frequency = 0.0f;
	public int audioSampleRate = 44100;
	public string microphone;
	public FFTWindow fftWindow;
	public Dropdown micDropdown;
	public Slider thresholdSlider;

	private List<string> options = new List<string>();
	private AudioSource audioSource;

	void Start(){

		//get components you'll need
		audioSource = GetComponent<AudioSource> ();

		// get all available microphones
		foreach (string device in Microphone.devices) {
			if (microphone == null) {
				//set default mic to first mic found.
				microphone = device;
			}
			options.Add (device);
		}
		microphone = options [PlayerPrefsManager.GetMicrophone ()];

		//add mics to dropdown
		micDropdown.AddOptions (options);
		micDropdown.onValueChanged.AddListener (delegate {
			micDropdownValueChangedHandler (micDropdown);
		});

	}

	public void Play() {
		//initialize input with default mic
		UpdateMicrophone ();
	}

	public void Stop(){
		audioSource.Stop(); 
		Microphone.End (microphone);
	}

	public AudioSource GetAudioSource(){
		return audioSource;
	}

	void UpdateMicrophone(){
		audioSource.Stop(); 
		//Start recording to audioclip from the mic
		audioSource.clip = Microphone.Start(microphone, true, 10, audioSampleRate);
		audioSource.loop = true; 
		// Mute the sound with an Audio Mixer group becuase we don't want the player to hear it
		Debug.Log(Microphone.IsRecording(microphone).ToString());

		if (Microphone.IsRecording (microphone)) { //check that the mic is recording, otherwise you'll get stuck in an infinite loop waiting for it to start
			while (!(Microphone.GetPosition (microphone) > 0)) {
			} // Wait until the recording has started. 
		
			Debug.Log ("recording started with " + microphone);

			// Start playing the audio source
			audioSource.Play (); 
		} else {
			//microphone doesn't work for some reason

			Debug.Log (microphone + " doesn't work!");
		}
	}


	public void micDropdownValueChangedHandler(Dropdown mic){
		microphone = options[mic.value];
		UpdateMicrophone ();
	}

	
	public float GetAveragedVolume()
	{ 
		float[] data = new float[256];
		float a = 0;
		audioSource.GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256;
	}

}