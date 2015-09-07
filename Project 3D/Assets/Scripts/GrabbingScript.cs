using UnityEngine;
using System.Collections;

public class GrabbingScript : MonoBehaviour {

	bool SmallBroInPos = false;
	public bool BigBroInPos = false;
	bool move = false;

	GameObject big;
	GameObject small;
	// Use this for initialization
	void Start () {
		big = GameObject.FindGameObjectWithTag ("Big");
		small = GameObject.FindGameObjectWithTag ("Small");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.I) && SmallBroInPos && BigBroInPos) {
			move = true;
			small.GetComponent<CheckIfGrounded>().grabbing = true;
			big.GetComponent<BigBroMovement>().enabled = false;
			small.GetComponent<SmallBroMovement>().enabled = false;
			big.GetComponent<Rigidbody>().isKinematic = true;
			big.GetComponent<Rigidbody>().useGravity = false;
		}

		if (Vector3.Distance(small.transform.position,big.transform.position) < 1f) {
			move = false;
			small.GetComponent<CheckIfGrounded>().grabbing = false;
			big.GetComponent<BigBroMovement>().enabled = true;
			big.GetComponent<Rigidbody>().isKinematic = false;
			big.GetComponent<Rigidbody>().useGravity = true;
		}

		if (move) {
			big.GetComponent<Rigidbody>().MovePosition(big.transform.position + ((small.transform.position - big.transform.position).normalized) * 5 * Time.deltaTime);
		}
	}

	void OnTriggerEnter() {
		SmallBroInPos = true;
	}

	void OnTriggerExit () {
		SmallBroInPos = false;
	}
}
