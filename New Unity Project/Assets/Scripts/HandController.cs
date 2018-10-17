using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour {

 
    public SteamVR_TrackedController controller;

    // if we choose to carry, which item should we carry?
    GameObject handItemReady;

    // flag to keep track of whether we are carrying something or not
    bool isCarrying = false;

    // previous position
    Vector3 previousPos;

    // velocity vector
    Vector3 handItemVelocity;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// check user input
        if (controller.triggerPressed)
        // if (controller.steamPressed)
        // if (controller.menuPressed)
        // if (controller.padPressed)
        // if (controller.padTouched)
        // if (controller.gripped)
        {            
            if(handItemReady != null && !isCarrying)
            {          
                // carry!
                Carry();
            } 
        }
        // we need to be carrying something in order to throw it!
        else if(isCarrying)
        {
            //calculate velocity
            handItemVelocity = (transform.position - previousPos) / Time.deltaTime;

            ThrowItem();
        }

        // save current position as the new "previous" position
        previousPos = transform.position;
    }

    void ThrowItem()
    {
        // get rigid body of the item
        Rigidbody rb = handItemReady.GetComponent<Rigidbody>();

        // our hand item should no longer be kinematic
        rb.isKinematic = false;

        // rigid body velocity (SteamVR SDK: device.velocity)
        rb.velocity = handItemVelocity;

        // remove parent
        handItemReady.transform.parent = null;

        // update flag
        isCarrying = false;
        
    }

    void OnTriggerStay(Collider other)
    {
        // items we can carry? update flag
        if (other.CompareTag("HandItem"))
        {            
            handItemReady = other.gameObject;
        }        
    }

    void OnTriggerExit(Collider other)
    {
        if (isCarrying) return;

        // items we can carry? update flag
        if (other.CompareTag("HandItem"))
        {
            handItemReady = null;
        }
    }

    void Carry()
    {
        // set the parent of the carried object to the controller
        handItemReady.transform.parent = transform;

        // make it a kinematic body so it doesn't respond to other forces
        handItemReady.GetComponent<Rigidbody>().isKinematic = true;

        // adjust position
        handItemReady.transform.position = transform.position + 0.05f * transform.forward - 0.05f * transform.up;

        // carrying flag
        isCarrying = true;
    }
}
