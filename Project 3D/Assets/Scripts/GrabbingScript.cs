using UnityEngine;
using System.Collections;

public class GrabbingScript : MonoBehaviour {
    
    //Components
    GameObject big;
    GameObject small;
    BigBroGlow bbGlow;

    //Variables
	bool move = false;

    // Use this for initialization
    void Start () {
		big = GameObject.FindGameObjectWithTag ("Big");
		small = GameObject.FindGameObjectWithTag ("Small");
        bbGlow = FindObjectOfType(typeof(BigBroGlow)) as BigBroGlow;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetButton ("SMALL_INTERACT_2") && bbGlow.bInPos && bbGlow.sInPos) { //pulling bigbro
			move = true;
			small.GetComponentInChildren<CheckIfGrounded>().grabbing = true;
			big.GetComponent<PlayerMovement>().enabled = false;
			small.GetComponent<PlayerMovement>().enabled = false;
			big.GetComponent<Rigidbody>().isKinematic = true;
			big.GetComponent<Rigidbody>().useGravity = false;
		}

		if (Vector3.Distance(small.transform.position,big.transform.position) < 2f) {
			move = false;
			small.GetComponentInChildren<CheckIfGrounded>().grabbing = false;
			big.GetComponent<PlayerMovement>().enabled = true;
			big.GetComponent<Rigidbody>().isKinematic = false;
			big.GetComponent<Rigidbody>().useGravity = true;
		}

		if (move) {
			big.GetComponent<Rigidbody>().MovePosition(big.transform.position + ((small.transform.position - big.transform.position).normalized) * 5 * Time.deltaTime);
		}
 
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Small"))
            bbGlow.sInPos = true;

        if (hit.gameObject.CompareTag("BigT"))
            bbGlow.bInPos = true;

    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.CompareTag("Small"))
            bbGlow.sInPos = false;
        if (hit.gameObject.CompareTag("BigT"))
            bbGlow.bInPos = false;

    }
}
