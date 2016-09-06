using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MICROPHONE_KEY = "microphone";
	const string VOLUME_KEY = "volume";
	const string SENSITIVITY_KEY = "sensitivity";

	public static void SetMicrophone (int mic) {
		PlayerPrefs.SetInt (MICROPHONE_KEY, mic);
	}

	public static int GetMicrophone (){
		return PlayerPrefs.GetInt (MICROPHONE_KEY);
	}

	public static void SetVolume (float volume) {
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (VOLUME_KEY, volume);
		} else {
			Debug.LogError("Volume out of range");
		}
	}

	public static float GetVolume (){
		return PlayerPrefs.GetFloat (VOLUME_KEY);
	}

	public static void SetSensitivity (float sensitivity) {
		if (sensitivity >= 1f && sensitivity <= 500f) {
			PlayerPrefs.SetFloat (SENSITIVITY_KEY, sensitivity);
		} else {
			Debug.LogError("Sensitivity out of range");
		}
	}
	
	public static float GetSensitivity (){
		return PlayerPrefs.GetFloat (SENSITIVITY_KEY);
	}

}
