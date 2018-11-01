using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// In this script, we deal with the walking-in-place locomotion technique.

public class Mouvements : MonoBehaviour
{
    // Left controller
    public SteamVR_TrackedObject controllerleft;
    // Right controller
    public SteamVR_TrackedObject controllerright;
    // Main Camera
    public Camera mainCamera;

    // Keep trace of the controllers' position
    float leftpos = 0;
    float rightpos = 0;

    void Start() {
    }

    // Update the movement
    void Update()
    {
        // If the player walks fast 
        if ( ((Math.Abs(controllerleft.transform.position.y - leftpos) > 0.01) && (Math.Abs(controllerright.transform.position.y - rightpos) > 0.01)))
        {
            // Avoid going into the ground 
            if (transform.position.y < 0)
            {
                transform.Translate(0, -transform.position.y, 0);
            }
            // Avoid going too high
            if (transform.position.y > 1.1)
            {
                transform.Translate(0, -transform.position.y + 1.1f, 0);
            }
            // Update camera position accordingly 
            transform.position = transform.position + Camera.main.transform.forward * 15f * Time.deltaTime;

        }

        // If the player walks slowly
        else if (((Math.Abs(controllerleft.transform.position.y - leftpos) > 0.001) && (Math.Abs(controllerright.transform.position.y - rightpos) > 0.001)))
        {
            // Avoid going into the ground 
            if (transform.position.y < 0)
            {
                transform.Translate(0, -transform.position.y, 0);
            }
            // Avoid going too high
            if (transform.position.y > 1.1)
            {
                transform.Translate(0, -transform.position.y + 1.1f, 0);
            }
            // Update camera position accordingly 
            transform.position = transform.position + Camera.main.transform.forward * 5f * Time.deltaTime;
        }

        // Update the variables that indicate the position of the controllers
        leftpos = controllerleft.transform.position.y;
        rightpos = controllerright.transform.position.y;

    }
}
