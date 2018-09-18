using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public GameObject canGeneratePower1;
	public GameObject canGeneratePower2;
	public GameObject canGeneratePower3;
	public GameObject flashlight;
	public Texture2D cursorTexture;

	//dertermine player enter power generator area
	bool isEnterPowerGenerator1 = false;
	bool isEnterPowerGenerator2 = false;
	bool isEnterPowerGenerator3 = false;

	public GameControl gc;
	public PowerGenerator pg1;
	public HanderBarRotate hbr1;
	public PowerGenerator pg2;
	public HanderBarRotate hbr2;
	public PowerGenerator pg3;
	public HanderBarRotate hbr3;

	// Use this for initialization
	void Start () {
		flashlight.SetActive (false); //Set flashlight as disable
		Cursor.lockState = CursorLockMode.Locked;
	}
	void OnGUI(){
		//Set aim on middle of screen
		Vector3 mPos = Input.mousePosition;
		GUI.DrawTexture (new Rect (mPos.x-32, Screen.height-mPos.y-32, 32, 32), cursorTexture);
	}
	
	// Update is called once per frame
	void Update () {
		
			if(Input.GetMouseButtonDown(0)){
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				//use raycast to detact which power generator player point to
				if (Physics.Raycast (ray, out hit)) {
					BoxCollider bc = hit.collider as BoxCollider;
					if (bc != null) {
						if(bc.gameObject.tag.Equals("Power Generator")){
							if ((bc.gameObject.name == "Power Generator1") && (isEnterPowerGenerator1)) {
								pg1.playsound ();
								hbr1.RotateHanderBar ();
								gc.GeneratePowerSupply(0);
							}
							if ((bc.gameObject.name == "Power Generator2") && isEnterPowerGenerator2) {
								pg2.playsound ();
								hbr2.RotateHanderBar ();
								gc.GeneratePowerSupply (1);
							}
							if ((bc.gameObject.name == "Power Generator3" && isEnterPowerGenerator3)) {
								pg3.playsound ();
								hbr3.RotateHanderBar ();
								gc.GeneratePowerSupply (2);
							}
						}

					}
				}

			}

		//Click F to start and close flash light
		if(Input.GetKeyUp(KeyCode.F) && !flashlight.activeSelf){
			flashlight.SetActive (true);
		}
		else if(Input.GetKeyUp(KeyCode.F) && flashlight.activeSelf){
			flashlight.SetActive (false);
		}
			
	}

	//set ture is player enter power generator area
	void OnTriggerEnter(Collider other){

		if (other.gameObject.name.Equals ("Power Generator1")) {
			isEnterPowerGenerator1 = true;
		}
		if (other.gameObject.name.Equals ("Power Generator2")) {
			isEnterPowerGenerator2 = true;
		}
		if (other.gameObject.name.Equals ("Power Generator3")) {
			isEnterPowerGenerator3 = true;
		}


	}

	//set false if player exit power generator area
	void OnTriggerExit(Collider other){

		if (other.gameObject.name.Equals ("Power Generator1")) {
			isEnterPowerGenerator1 = false;
		}
		if (other.gameObject.name.Equals ("Power Generator2")) {
			isEnterPowerGenerator2 = false;
		}
		if (other.gameObject.name.Equals ("Power Generator3")) {
			isEnterPowerGenerator3 = false;
		}


	}

}
