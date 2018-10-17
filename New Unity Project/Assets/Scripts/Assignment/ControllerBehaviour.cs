using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ControllerBehaviour : MonoBehaviour {

	//The steamVR controller
	public SteamVR_TrackedController controller;
	//The game manager 
	public GameFlowManager gameFlowManager;
	private float dragSpeed = 2.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		// check user input
        if(controller.triggerPressed)
        {   
			//Here, we use controller to "raycast" the target
			//if the raycast hit any thing, we check whether it is the target or not (by tag)
			//if it is, then move the target toward the controller itself
			//Otherwise, nothing happen
			RaycastHit hit;
			if(Physics.Raycast(this.transform.position, this.transform.forward, out hit, 100.0f)){
				//Raycast to the target
				if(hit.collider.gameObject.CompareTag("Target")){
					hit.collider.gameObject.transform.position += -this.transform.forward * dragSpeed * Time.deltaTime;
					//update the linerenderer for visualization
					setLineRenderer(this.transform.position, this.transform.forward, hit.distance);
				}
				else{
					//update the linerenderer for visualization
					setLineRenderer(this.transform.position, this.transform.forward, 100.0f);
				}
			}
			else{
				//update the linerenderer for visualization
				setLineRenderer(this.transform.position, this.transform.forward, 100.0f);
			}

        }
		else{
			this.GetComponent<LineRenderer>().enabled = false;
		}
    }

    void OnTriggerEnter(Collider other)
    {
		//If the controller touch the target
		//generate a new target
		if(other.gameObject.CompareTag("Target")){
			gameFlowManager.NextTurn();
		}
    }
	void setLineRenderer(Vector3 ori, Vector3 dir, float distance){
		//The line renderer consists of multiple point to simulate a line or a curve
		//Here, we use two points to render a line
		List<Vector3> points = new List<Vector3>();
		//The first point will be the origin
		points.Add(ori);
		//The second point will be ori + distance * dir
		points.Add(ori + dir * distance);
		//Set the position of the line renderer
		this.GetComponent<LineRenderer>().SetPositions(points.ToArray());
		this.GetComponent<LineRenderer>().enabled = true;
	}

}
