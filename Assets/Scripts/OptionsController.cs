using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Dropdown microphone;
	public Slider volumeSlider,sensitivitySlider, thresholdSlider;
	public GameObject settingsPanel;
	public GameObject openButton;

	private bool panelActive = false;

	// Use this for initialization
	void Start () {
		microphone.value = PlayerPrefsManager.GetMicrophone ();
		volumeSlider.value = PlayerPrefsManager.GetVolume ();
		sensitivitySlider.value = PlayerPrefsManager.GetSensitivity ();
	}

	public void SaveAndExit (){
		PlayerPrefsManager.SetMicrophone (microphone.value);
		PlayerPrefsManager.SetVolume (volumeSlider.value);
		PlayerPrefsManager.SetSensitivity (sensitivitySlider.value);

		panelActive = !panelActive;
		settingsPanel.GetComponent<Animator> ().SetBool ("PanelActive",panelActive);
	}

	public void SetDefaults(){
		microphone.value = 0;
		volumeSlider.value = 1f;
		sensitivitySlider.value = 50f;
	}

	public void OpenSettings(){
		panelActive = !panelActive;
		settingsPanel.GetComponent<Animator> ().SetBool ("PanelActive",panelActive);
	}

	public void TogglePanel(){
		if (!panelActive) {
			OpenSettings ();
		} else {
			SaveAndExit ();
		}
	}
}
