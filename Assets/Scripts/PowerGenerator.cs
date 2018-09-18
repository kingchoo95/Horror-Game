using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour {

	public AudioSource generatorSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//play generator sound
	public void playsound(){
		generatorSound.Play ();
	}
	//rotate power generator hander bar 
	public void RotateHanderBar(){
		transform.Rotate (0f,3000f,0f);
	}

}
