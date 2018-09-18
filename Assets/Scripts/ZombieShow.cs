using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieShow : MonoBehaviour {
	public GameObject GhostEvent;
	// Use this for initialization

	int countTimes = 0; 
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	
	}	
	void OnTriggerEnter(Collider other){

		if (other.name == "FPSController") {
			//if collide the fifth times, ghost event trigger
			countTimes += 1;
			if (countTimes ==5) {
				GhostEvent.gameObject.SetActive (true);
			}
		}
	}
}
