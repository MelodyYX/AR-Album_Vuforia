using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate64Cubes : MonoBehaviour {

	public GameObject myCubePrefab;
	GameObject[] sampleCubes = new GameObject[64];
	public int spectrumScale;

	// Use this for initialization
	void Start () {

		// Create a circle of cubes
		for(int i=0; i<64; i++){
			GameObject instanceOfCubePrefab = (GameObject)Instantiate (myCubePrefab);

			instanceOfCubePrefab.transform.position = this.transform.position; ///Set this cube in the center of the object which it attach to.
			//Debug.Log ("this transform: "+this.transform.gameObject.name);
			instanceOfCubePrefab.transform.parent = this.transform;  ///Make this cube a child of the object which it attach to.

			this.transform.eulerAngles = new Vector3 (0, -5.625f*i, 0); /// The angle difference between each cube
			instanceOfCubePrefab.transform.position = Vector3.forward*0.5f;  ///set a certain distance to center

			instanceOfCubePrefab.name = "Sample Cube: " + i; // Change the name of every instance
			sampleCubes [i] = instanceOfCubePrefab; // set this instance of cube into sampleCube list
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0; i<64; i++){
			if(sampleCubes != null){
				sampleCubes [i].transform.localScale = new Vector3 (0.06f, (GetAudioSpectrumSamplesData.mySpectrumSamples[i] * spectrumScale) + 0.06f, 0.06f);
				
			}
		}
		
	}
}
