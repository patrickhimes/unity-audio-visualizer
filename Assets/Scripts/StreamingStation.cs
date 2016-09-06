using UnityEngine;
using System.Collections;

public class StreamingStation : MonoBehaviour {
	public string stationURL;
	private AudioSource source;
	enum AudioState {Play, Stop, Pause};
	private AudioState audioState = AudioState.Stop;

	void Start(){
		WWW www = new WWW(stationURL);
		source = GetComponent<AudioSource>();
		source.clip = www.audioClip;
		Play ();
	}

	public void Play() {
		audioState = AudioState.Play;
	}

	void Update() {
		
		if (audioState == AudioState.Play && !source.isPlaying && source.clip.loadState == AudioDataLoadState.Loaded) {
			source.Play ();
		}

	}

	public AudioSource GetAudioSource(){
		return source;
	}

	public void Stop(){
		source.Stop ();
		audioState = AudioState.Stop;
	}

	public void Pause(){
		source.Pause ();
		audioState = AudioState.Pause;
	}

}
