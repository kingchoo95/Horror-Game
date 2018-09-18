using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanderBarRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//rotate power generator hander bar 
	public void RotateHanderBar(){
		transform.Rotate (0f,3000f,0f);
	}
}
