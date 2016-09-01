using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NoteIdentifier : MonoBehaviour {

	public MicrophoneInput micIn;
	public string noteDetected;
	public bool displayOctave;

	private float[,] notes;
	private string[] noteNames;
	private int maxOctave;
	private int maxNote;
	private List<string> notesList;

	void Start () {
		//look for MicrophoneInput on GameObject if micIn is null
		if( micIn == null ){
			micIn = gameObject.GetComponent("MicrophoneInput") as MicrophoneInput;
		} 

		notes = new float[,]
		{
			//C		C#		D		Eb		E		F		F#		G		G#		A		Bb		B
			{16.35f,17.32f,	18.35f,	19.45f,	20.60f,	21.83f,	23.12f,	24.50f,	25.96f,	27.50f,	29.14f,	30.87f},
			{32.70f,34.65f,	36.71f,	38.89f,	41.20f,	43.65f,	46.25f,	49.00f,	51.91f,	55.00f,	58.27f,	61.74f},
			{65.41f,69.30f,	73.42f,	77.78f,	82.41f,	87.31f,	92.50f,	98.00f,	103.8f,	110.0f,	116.5f,	123.5f},
			{130.8f,138.6f,	146.8f,	155.6f,	164.8f,	174.6f,	185.0f,	196.0f,	207.7f,	220.0f,	233.1f,	246.9f},
			{261.6f,277.2f,	293.7f,	311.1f,	329.6f,	349.2f,	370.0f,	392.0f,	415.3f,	440.0f,	466.2f,	493.9f},
			{523.3f,554.4f,	587.3f,	622.3f,	659.3f,	698.5f,	740.0f,	784.0f,	830.6f,	880.0f,	932.3f,	987.8f},
			{1047f,	1109f,	1175f,	1245f,	1319f,	1397f,	1480f,	1568f,	1661f,	1760f,	1865f,	1976f},
			{2093f,	2217f,	2349f,	2489f,	2637f,	2794f,	2960f,	3136f,	3322f,	3520f,	3729f,	3951f},
			{4186f,	4435,	4699f,	4978f,	5274f,	5588f,	5920f,	6272f,	6645f,	7040f,	7459f,	7902f}
		};

		noteNames = new string[]
		{
			"C","C#","D","Eb","E","F","F#","G","G#","A","Bb","B"
		};

		maxOctave = notes.GetUpperBound(0);
		maxNote = notes.GetUpperBound(1);
		notesList = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
		if(micIn != null){
			FindNote();
		}
	}

	void FixedUpdate(){
		if(notesList.Count() > 0)
		{
			string[] notesArray = notesList.ToArray();
			DebugArray(notesArray);
			noteDetected = notesArray.GroupBy(v => v)
				.OrderByDescending(g => g.Count())
					.First()
					.Key;
		}
		notesList.Clear();
	}

	void DebugArray( string[] outputArray){
		string debugText = "";

		for( int i = 0; i < outputArray.Length; i++)
		{
			debugText +=  outputArray[i] + ", ";
		}
		Debug.Log(debugText);
	}

	void FindNote()
	{
		float frequency = micIn.GetFundamentalFrequency();
		string note = "";
		string octave = "";
		if(frequency > 0f){

			for (int octaveIndex = 0; octaveIndex <= maxOctave; octaveIndex++)
			{
				//compair frequency to the last cell in the octave
				// if it's lower, then we know the note is in that octave
				float highestFrequencyInOctave = GetUpperRageOfNote( notes[octaveIndex,maxNote] );

				if( frequency < highestFrequencyInOctave)
				{
					//okay, the frequency is somewhere in this octave
					octave = octaveIndex.ToString();

					for (int noteIndex = 0; noteIndex <= maxNote; noteIndex++)
					{
						float upperRangeOfNote = GetUpperRageOfNote( notes[octaveIndex,noteIndex] );

						if( frequency < upperRangeOfNote){

							note = noteNames[noteIndex];
							//we found our note, lets leave the loop
							break;
						}
					}

					//we found our octave, lets leave the loop
					break;
				}

			}
			if(displayOctave){
				notesList.Add( note + octave );
			}else{
				notesList.Add( note );
			}
		}else{
			// no input detected
			//clear label
			noteDetected = "";
		}
	}

	float GetUpperRageOfNote(float f)
	{
		//this is broken into several steps to keep it easy to read
		float nextNote = 1.059463f * f;
		float noteDiff = nextNote - f;
		return f + (noteDiff/2);
	}
}
