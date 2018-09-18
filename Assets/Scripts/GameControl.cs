using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
	
	// Count Down timer
	float timePassed = 0;
	int currentHour = 3;
	int currentMin = 55;

	//store Energy left on each generator
	int[] powerLeft = {0,0,0};

	//store number to reduce power supply
	int[] powerReduce = {0,0,0}; 

	//ramdom number generate for power supply
	int RandomNumberPowerGenerator = 0;

	//ramdom number generator to reduce power supply
	int RamdomNumberPowerReduce = 0; 

	//UI Timer,gameover,win	 and Power supply
	public Text clock;
	public Text powerSupply;
	public Text gameOverText;
	public Text winText;
	public Button backToMenu;

	//gameobject power generator
	public GameObject powerGenerator1;
	public GameObject powerGenerator2;
	public GameObject powerGenerator3;
	public AudioSource generatorDeadSound;

	// ghost for game over sence
	public GameObject Ghost;


	//light 
	public Light Light1;
	public Light Light2;
	public Light Light3;

	//identify powersupply still working
	public bool powerGenerater1IsDead = false;
	public bool powerGenerater2IsDead = false;
	public bool powerGenerater3IsDead = false;

	// Use this for initialization
	void Start () {

		//generate random number for initial power supply
		for (int x = 0 ; x < powerLeft.Length ; x++) {
			RandomNumberPowerGenerator = Random.Range (70, 90);
			powerLeft [x] = RandomNumberPowerGenerator;
		}

		//generate random number and assign to each generator power usage

		for (int x = 0 ; x < powerLeft.Length ; x++) {
			RamdomNumberPowerReduce = Random.Range (1, 4);
			//ensure each generator has uniqe power reduce number
			if (x == 0) {
				powerReduce [x] = RamdomNumberPowerReduce;	
			} else {
				while((RamdomNumberPowerReduce==powerReduce [x-1])||(RamdomNumberPowerReduce==powerReduce [0])){
					RamdomNumberPowerReduce = Random.Range (1, 4);
				}
				powerReduce [x] = RamdomNumberPowerReduce;	
			}
		
		}

		// call ReductPowerSupply method every 0.3 second, start from 5 second
		InvokeRepeating ("ReductPowerSupply",5.0f,0.3f);
	
	}
	
	// Update is called once per frame
	void Update () {

		// count time left for UI
		timePassed += Time.deltaTime;
		//make it 20 secound equal to 1 minutes because 5 mins is too long
		if(timePassed > 20){			
			currentMin = currentMin + 1;
			if (currentMin > 59) {				
				currentMin = 0;
				currentHour = 4;

				//run win method after 3 secound
				InvokeRepeating ("win",3.0f,0f);
			}
			//show time passed on screen
			clock.text = currentHour+ " : " +currentMin.ToString("00")+" AM";	
			timePassed = 0;

		}

		//if no more power supply deadsound will play and distroy powerGenerator
		for(int x=0; x< powerLeft.Length ; x++){
			if (powerLeft [x] < 0) {
				powerLeft[x] = 0;
				playGeneratorDeadsound ();

				//destroy power generator when power is 0
				int generatorNum = x + 1;
				Destroy (GameObject.Find("Power Generator"+generatorNum));

				//disable light when power is 0, set dead power generator to true 
				if (generatorNum == 1) {
					Light1.gameObject.SetActive (false);
					powerGenerater1IsDead = true;
				} else if (generatorNum == 2) {
					Light2.gameObject.SetActive (false);
					powerGenerater2IsDead = true;
				} else {
					Light3.gameObject.SetActive (false);	
					powerGenerater3IsDead = true;
				}

				//if left one working power generator, game over
				if((powerGenerater1IsDead && powerGenerater2IsDead) || (powerGenerater2IsDead && powerGenerater3IsDead) || (powerGenerater1IsDead && powerGenerater3IsDead)){

					//run scaryEventBeforeEnd method after 4 second
					InvokeRepeating ("scaryEventBeforeEnd",4.0f,0f);

					//run gameOver method after 7 second
					InvokeRepeating ("gameOver",7.0f,0f);

				}
					
			}
			//show power supply on screen
			powerSupply.text = "Power Supply\nGenerator 1 : "+powerLeft[0] +"%\nGenerator 2 : "+ powerLeft[1]+ "%\nGenerator 3 : "+  powerLeft[2]+"%";
		}
				
	}

	//method generate power 
	public void GeneratePowerSupply(int number){
		//each click plus 3 power
		int AllGeneratorStillAlife = 4;
		//if one of the generator down player only can add 2 power per click
		if ((powerGenerater1IsDead) || (powerGenerater2IsDead) || (powerGenerater3IsDead)) {
			AllGeneratorStillAlife = 3;
		}
		// stop adding power if power more than 100 , if power more than 100, power  set to 100
		if (powerLeft [number] < 99) {
			
			powerLeft [number] += AllGeneratorStillAlife;

		} else if (powerLeft [number] > 99) {
			powerLeft [number] = 100;
		}
	}

	//mothod for reduce power supply
	public void ReductPowerSupply(){
		
		//reduce time per secound
		for (int x = 0; x < powerLeft.Length; x++) {
			powerLeft [x] -= powerReduce [x];
		}

	}

	//method for play dead sound when powergenerator equal zero
	public void playGeneratorDeadsound(){
		generatorDeadSound.Play ();
	}

	// method for game over
	public void gameOver(){
		int totalScore = 0;
		for (int x = 0; x < powerLeft.Length; x++) {
			totalScore += powerLeft [x];
		}
		gameOverText.text ="Game Over\nTotal Score : "+ totalScore;
		Time.timeScale = 0;
		backMenu ();

	}

	// method for scary event before game over text appear
	public void scaryEventBeforeEnd(){
		
		Light1.gameObject.SetActive (true);
		Light2.gameObject.SetActive (true);
		Light3.gameObject.SetActive (true);
		Ghost.gameObject.SetActive (true);

	}

	//win text appear
	public void win(){
		int totalScore = 0;
		for (int x = 0; x < powerLeft.Length; x++) {
			totalScore += powerLeft [x];
		}
		winText.text ="You Survived\nTotal Score : "+ totalScore;
		Time.timeScale = 0;
		backMenu ();
	}

	public void backMenu(){
		backToMenu.gameObject.SetActive (true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = false;
	}

}
