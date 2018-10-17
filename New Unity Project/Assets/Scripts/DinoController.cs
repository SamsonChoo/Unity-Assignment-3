using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DinoController : MonoBehaviour {

    // walking speed
    public float speed;

    // max magnitude
    public float maxMagnitude = 2;

    // target destination
    Vector3 target;

    // flag of "alive" status
    bool isAlive = true;

    // animator
    Animator anim;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

        InvokeRepeating("FindTarget", 0, 0.2f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // only if alive
        if (!isAlive) return;

        // v = d / t => d = v * t
        float d = speed * Time.fixedDeltaTime;

        // direction
        Vector3 dir = target - transform.position;

        // unitary vector (magnitude == 1)
        Vector3 dirUnit = dir.normalized;

        // movement
        Vector3 movement = d * dirUnit;

        // keep them at ground level
        movement.y = 0;

        // move the dino
        transform.position += movement;
	}

    void FindTarget()
    {
        // set player position
        target = Camera.main.transform.position;

        // don't make them rotate to look up or down
        target.y = transform.position.y;

        // make it look at the target
        transform.LookAt(target);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isAlive) return;

        if(collision.relativeVelocity.magnitude > maxMagnitude)
        {
            // set alive flag (defeated!)
            isAlive = false;

            // trigger the animation state machine change
            anim.SetTrigger("Defeat");

            // disable collider
            GetComponent<Collider>().enabled = false;

            // stop invoking the find target
            CancelInvoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MainCamera"))
        {
            print("game over");

            //restart
            SceneManager.LoadScene("Game");
        }
    }
}
