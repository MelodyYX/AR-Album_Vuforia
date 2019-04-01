using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]

public class GetAudioSpectrumSamplesData : MonoBehaviour {

	AudioSource myAudioSource;
	public static int samplesNumber =128; // Must be a power of 2, min= 64,max= 8192.(ie.128/256)
	public static int bandsNumber = 8; // Use this number when samplesNumber not in the range. Come with myFrequencyBands[] and GetFrequencyBands ()
	public static float[] mySpectrumSamples = new float[samplesNumber];
	public static float[] myFrequencyBands = new float[bandsNumber];

	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent <AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		GetSpectrumAudioSource ();
		//GetFrequencyBands ();// Use this method when samples' number<64
	}

	void GetSpectrumAudioSource(){
		myAudioSource.GetSpectrumData (mySpectrumSamples,0,FFTWindow.Blackman);
	}

	void GetFrequencyBands(){
		
		int count = 0;

		for (int i=0; i < 8; i++){

			float sum = 0;
			int samplesNumber = (int)Mathf.Pow (2,i+1);

			if (i == 7) {
				samplesNumber += 2;
			}

			for (int j = 0; j < samplesNumber; j++) {
				sum += mySpectrumSamples[count];
				count++;
			}

			myFrequencyBands[i] = sum / samplesNumber;
		}
	}
}
  
