using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]

public class GetAudioSpectrumSamplesData : MonoBehaviour {

	AudioSource myAudioSource;
	public static float[] mySpectrumSamples = new float[64];

	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent <AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		GetSpectrumAudioSource ();
	}

	void GetSpectrumAudioSource(){
		myAudioSource.GetSpectrumData (mySpectrumSamples,0,FFTWindow.Blackman);
	}
}
  
