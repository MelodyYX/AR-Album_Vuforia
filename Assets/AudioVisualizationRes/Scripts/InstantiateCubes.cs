using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour {

	public GameObject myCubePrefab;
	GameObject[] sampleCubes = new GameObject[GetAudioSpectrumSamplesData.samplesNumber];
	public int spectrumScale;

	// Use this for initialization
	void Start () {

		// Create a circle of cubes
		for(int i=0; i<GetAudioSpectrumSamplesData.samplesNumber; i++){
			
			GameObject instanceOfCubePrefab = (GameObject)Instantiate (myCubePrefab);

			instanceOfCubePrefab.transform.position = this.transform.position; ///Set this cube in the center of the object which it attach to.
			instanceOfCubePrefab.transform.parent = this.transform;  ///Make this cube a child of the object which it attach to.
			this.transform.eulerAngles = new Vector3 (0, -(360f/GetAudioSpectrumSamplesData.samplesNumber)*i, 0); /// The angle difference between each cube
		    instanceOfCubePrefab.transform.position = Vector3.forward*0.5f;  ///set a certain distance to center

			instanceOfCubePrefab.name = "Sample Cube: " + i; // Change the name of every instance
			sampleCubes [i] = instanceOfCubePrefab; // set this instance of cube into sampleCube list
			 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		for(int i=0; i<GetAudioSpectrumSamplesData.samplesNumber; i++){
			if(sampleCubes!=null){
			   sampleCubes [i].transform.localScale = new Vector3 (0.05f, (GetAudioSpectrumSamplesData.mySpectrumSamples[i] *(i+0.01f) *spectrumScale) + 0.01f, 0.05f);
			}

		}
			

		/*
		 * for(int i=0; i<GetAudioSpectrumSamplesData.bandsNumber; i++){
			if(sampleCubes!=null){
				sampleCubes [i].transform.localScale = new Vector3 (0.05f, (GetAudioSpectrumSamplesData.myFrequencyBands[i] *(i+0.01f) *spectrumScale) + 0.01f, 0.05f);
			}
		}
		*/

	}
}
