using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour {

	//The Player position
	public GameObject playerCamera;
	//The script to generate a new target
	private TargetController targetController;
	//The current amount of the collected target and the the target amount
	private int count;
	private const int TargetCount = 5;
	// Use this for initialization
	void Start () {
		//Get the targetController 
		targetController = this.GetComponent<TargetController>();
		//Initialize the amount
		count = -1;
		//set the poisition of the target cube
		targetController.setTargetPosition(playerCamera.transform.position);
		//generate a new target
		NextTurn();
	}

	public void NextTurn(){
		//Generate a new target or disable the target
		if(count < TargetCount){
			count++;
			targetController.renewTarget();
		}
			
		else
			targetController.disableTarget();
	}
}
