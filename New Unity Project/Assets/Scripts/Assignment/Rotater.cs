using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Rotate the gameObject 15 degrees, 30 degrees, 45 degrees in x,y,z axis every second
		this.transform.Rotate(new Vector3 (15, 30, 45) * Time.deltaTime);
	}
}
