using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]

public class GetAudioSpectrumSamplesData : MonoBehaviour {

	AudioSource myAudioSource;
	public static int samplesNumber =64; // Must be a power of 2, min= 64,max= 8192.(ie.128/256)
	public static float[] mySpectrumSamples = new float[samplesNumber];

	float[] highestSpectrumValue = new float[samplesNumber];
	public static float[] convertedSpectrumValue = new float[samplesNumber];

	public static float[] myBuffer = new float[samplesNumber];
	float[] bufferDecrease = new float[samplesNumber];

	public static int bandsNumber = 8; // Use this number when samplesNumber lower the range. Work with myFrequencyBands[] and GetFrequencyBands ()
	public static float[] myFrequencyBands = new float[bandsNumber];
	float[] my512Samples = new float[512];



	// Use this for initialization
	void Start () {
		 myAudioSource = GetComponent <AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		 GetSpectrumAudioSource ();
		 ConvertSpectrumValue ();  //Conver the spetrums value into a range between 0~1
		 SmoothTheSpectrums ();  // Smoothing the spetrums 

		//GetEightFrequencyBands (); //Using this function when bandsNumber=8.
		//GetFrequencyBands(); //Using this function when bandsNumber<64 && power of 2.

	} 

	void GetSpectrumAudioSource(){
		 myAudioSource.GetSpectrumData (mySpectrumSamples,0,FFTWindow.Blackman);
	}

	//Conver spetrum value into a range between 0~1
	void ConvertSpectrumValue(){
		for(int i=0; i<samplesNumber; i++){
			if (mySpectrumSamples[i]> highestSpectrumValue[i]){
				highestSpectrumValue [i] = mySpectrumSamples [i] ;
			}
		}

		for (int i = 0; i < samplesNumber; i++) {
			convertedSpectrumValue [i] = mySpectrumSamples [i] / highestSpectrumValue [i];
		}
	}
		

	void SmoothTheSpectrums (){
		
		for (int i=0; i<samplesNumber; i++)
		{

			if (mySpectrumSamples [i] >  myBuffer [i]) 
			{
				myBuffer [i] = mySpectrumSamples [i];
				bufferDecrease [i] = 0.001f;

			} 

			if (mySpectrumSamples [i] < myBuffer [i]) 
			{
				if ((myBuffer [i] - bufferDecrease [i]) > 0) 
				{
					myBuffer [i] -= bufferDecrease [i];
					mySpectrumSamples [i] = myBuffer [i];
					bufferDecrease [i] *= 1.2f;

				} else {
					myBuffer [i] = 0;
					mySpectrumSamples [i] = 0;
					bufferDecrease [i] = 0.001f;
				}

			}

		}
		 
	}
		

	//Using this function when bandsNumber=8.
	void GetEightFrequencyBands(){  		

		myAudioSource.GetSpectrumData (my512Samples,0,FFTWindow.Blackman);

		int count = 0;

		for (int i=0; i < 8; i++)
		{

			float sum = 0;
			int summedSamplesNumber = (int)Mathf.Pow (2,i+1);
			for (int j = 0; j < summedSamplesNumber; j++) 
			{
				sum += mySpectrumSamples[count];
				count++;
			}

			myFrequencyBands[i] = sum / summedSamplesNumber;
		}
	}


	// Using this method when bandsNumber<64 && power of 2.       
	void GetFrequencyBands(){  	

		myAudioSource.GetSpectrumData (my512Samples,0,FFTWindow.Blackman);

		int count = 0;

		for (int i=0; i < bandsNumber; i++)
		{

			float sum = 0;
			int summedSamplesNumber = (int)512/bandsNumber;
			for (int j = 0; j < summedSamplesNumber; j++) 
			{
				sum += my512Samples[count];
				count++;
			}
			myFrequencyBands[i] = sum / summedSamplesNumber;
		}
	}

}
  
