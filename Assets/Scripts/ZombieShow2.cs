using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieShow2 : MonoBehaviour {
	bool GhostEventOn = false;
	int countTimes = 0; 
	public GameObject GhostEvent;
	int ramNum;

	// Use this for initialization
	void Start () {
		
		ramNum = Random.Range (16, 28);
		while (ramNum % 4 != 0) {
			ramNum = Random.Range (4, 10);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GhostEventOn) {
			transform.Translate (2f,0f,0f);
		}
	}
	void OnTriggerEnter(Collider other){
		Debug.Log (ramNum);
		if (other.name == "FPSController") {
			//if collide the fifth times, ghost event trigger
			countTimes += 1;
		
			if (countTimes ==ramNum) {
				GhostEvent.gameObject.SetActive (true);
			}
		}
		 
}

}
