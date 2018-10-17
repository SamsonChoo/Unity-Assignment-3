using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In this script
//We 

public class TargetController : MonoBehaviour {

	//The target cube in the scene
	public GameObject targetCube;
	//The distance to the center
	public float radius = 5.0f;
	public void renewTarget(){

		//Get a random angle
		float angle = Random.Range(0, 360.0f);
		//Set the position according to the angle
		Vector3 pos = Vector2.zero;
		pos.x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
		pos.y = 0;
		pos.z = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
		Vector3 prePos = targetCube.transform.position;
		targetCube.transform.position = prePos + pos;
		//Enable the target cube
		if(!targetCube.active)
			targetCube.SetActive(true);
	}
	public void disableTarget(){
		targetCube.SetActive(false);
	}
	public void setTargetPosition(Vector3 pos){
		targetCube.transform.position = pos;
	}

}
